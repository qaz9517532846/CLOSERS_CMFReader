using SharpCompress.Compressors.Deflate;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Leayal.Closers.CMF.Helper;

namespace Leayal.Closers.CMF
{
    /// <summary>
    /// Provide interaction with the Closers's CMF file
    /// </summary>
    public class CMFArchive : IDisposable
    {
        /// <summary>
        /// Open CMF file to edit.
        /// </summary>
        /// <param name="cmfFile">Path to the CMF file.</param>
        /// <returns></returns>
        public static CMFArchive Open(string cmfFile)
        {
            FileStream fs = File.Open(cmfFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            return Read(fs, false);
        }
        /// <summary>
        /// Open CMF file to read.
        /// </summary>
        /// <param name="cmfFile">Path to the CMF file.</param>
        /// <returns></returns>
        public static CMFArchive Read(string cmfFile)
        {
            FileStream fs = File.OpenRead(cmfFile);
            return Read(fs, false);
        }
        /// <summary>
        /// Read CMF file from stream.
        /// </summary>
        /// <param name="stream">Content Stream</param>
        /// <returns></returns>
        public static CMFArchive Read(Stream stream) => Read(stream, false);
        /// <summary>
        /// Read CMF file from stream.
        /// </summary>
        /// <param name="stream">Content Stream</param>
        /// <param name="leaveOpen">Determine if the stream will be closed when <see cref="CMFArchive"/> is closed.</param>
        /// <returns></returns>
        public static CMFArchive Read(Stream stream, bool leaveOpen)
        {
            if (!stream.CanRead)
                throw new InvalidOperationException("The stream should be readable.");
            CMFArchive result = new CMFArchive(stream, leaveOpen);
            result.ReadHeader();
            return result;
        }

        private CMFReader myReader;
        private CMFEditor myEditor;
        private bool leaveStreamOpen;
        private BinaryReader binaryReader;
        internal byte[] _signature;
        internal ReadOnlyCollection<CMFEntry> entrylist;
        internal long dataoffsetStart;
        internal long headeroffsetStart;

        /// <summary>
        /// The base stream which this archive instance used to open CMF file.
        /// </summary>
        public Stream BaseStream { get; }
        private int filecount;
        /// <summary>
        /// Return the number of entries in the CMF Archive.
        /// </summary>
        public int FileCount => this.filecount;
        /// <summary>
        /// Return the list of entry in the CMF archive.
        /// </summary>
        public ReadOnlyCollection<CMFEntry> Entries
        {
            get
            {
                if (this._disposed)
                    throw new System.ObjectDisposedException("Archive");

                if (this.entrylist == null)
                    this.entrylist = this.ReadEntryList();
                return this.entrylist;
            }
        }
        /// <summary>
        /// Return the <seealso cref="CMFEntry"/> at the specific index.
        /// </summary>
        /// <param name="index">The index of the entry</param>
        /// <returns></returns>
        public CMFEntry this[int index]
        {
            get
            {
                if (this._disposed)
                    throw new System.ObjectDisposedException("Archive");

                return this.Entries[index];
            }
        }
        /// <summary>
        /// Return the <seealso cref="CMFEntry"/> which match the given full-path inside the archive. Return null if no entry matches.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public CMFEntry this[string path]
        {
            get
            {
                if (this._disposed)
                    throw new System.ObjectDisposedException("Archive");

                if (string.IsNullOrEmpty(path)) return null;

                StringBuilder sb = new StringBuilder();

                if (path.IndexOf("\\\\") > -1)
                    path = path.Replace("\\\\", "\\");
                if (path.IndexOf("//") > -1)
                    path = path.Replace("//", "/");
                if (path.IndexOf('\\') > -1)
                    path = path.Replace('\\', '/');
                for (int i = 0; i < this.Entries.Count; i++)
                {
                    sb.Clear();
                    sb.Append(this.Entries[i].FileName);
                    sb.Replace('\\', '/');
                    if (string.Equals(sb.ToString(), path, StringComparison.OrdinalIgnoreCase))
                        return this.Entries[i];
                }
                return null;
            }
        }

        private CMFArchive(Stream baseStream, bool leaveOpen)
        {
            this.dataoffsetStart = 0;
            this.headeroffsetStart = 0;
            this.leaveStreamOpen = leaveOpen;
            this.myReader = null;
            this.BaseStream = baseStream;
        }

        private void ReadHeader()
        {
            this.binaryReader = new BinaryReader(this.BaseStream);

            // Skip the first 100 bytes. Signature, perhaps?
            this._signature = this.binaryReader.ReadBytes(100);

            this.filecount = CmfHelper.Decode(binaryReader.ReadUInt32(), CmfFormat.EntryKey1);

            this.headeroffsetStart = this.BaseStream.Position;

            this.dataoffsetStart = this.BaseStream.Position + (CmfFormat.FileHeaderSize * this.filecount);
        }

        private ReadOnlyCollection<CMFEntry> ReadEntryList()
        {
            this.BaseStream.Seek(this.headeroffsetStart, SeekOrigin.Begin);
            /*
             * File table has fixed row size 528 bytes.
             * string(512):Filename - int(4):FileSize - int(4):CompressedFileSize - int(4):FileOffset - int(4):Flag
             */
            // CmfFormat.FileHeaderSize

            CMFEntry[] entryList = new CMFEntry[this.filecount];
            string tmp_filename;
            byte[] bytebuffer = new byte[CmfFormat.FileHeaderSize];
            int readcount, indexofNull;
            int offset = (int)this.BaseStream.Position;
            CMFEntry currentCMFEntry;
            for (int i = 0; i < entryList.Length; i++)
            {
                tmp_filename = null;
                currentCMFEntry = new CMFEntry();

                readcount = this.binaryReader.Read(bytebuffer, 0, bytebuffer.Length);
                if (readcount == bytebuffer.Length)
                {
                    currentCMFEntry.headeroffset = offset;
                    // Decode the buffer.
                    CmfHelper.Decode(ref bytebuffer);

                    // First 512 bytes is the filename
                    tmp_filename = Encoding.ASCII.GetString(bytebuffer, 0, CmfFormat.FileHeaderNameSize);

                    // This doesn't look good.
                    indexofNull = tmp_filename.IndexOf("\0\0");
                    if (tmp_filename.IndexOf("\0\0") == -1)
                    {
                        indexofNull = tmp_filename.LastIndexOf('\0');
                        tmp_filename = Encoding.ASCII.GetString(bytebuffer, 0, indexofNull);
                    }
                    else
                        currentCMFEntry._filename = Encoding.Unicode.GetString(bytebuffer, 0, indexofNull + 1);

                    currentCMFEntry._filename = currentCMFEntry._filename.RemoveNullChar();

                    // Next is 4 bytes for the unpacked size (aka original file)
                    currentCMFEntry._unpackedsize = BitConverter.ToInt32(bytebuffer, 512);

                    // Next is another 4 bytes for compressedsize
                    currentCMFEntry._compressedsize = BitConverter.ToInt32(bytebuffer, 516);

                    // Next is another 4 bytes for "DataOffset"
                    currentCMFEntry.dataoffset = BitConverter.ToInt32(bytebuffer, 520);

                    // if (str == "FX" || str == "LUA" || str == "TET" || str == "XET")

                    // Last is the flag, determine if the file is compressed (with Zlib) or encrypted or nothing special.
                    switch (BitConverter.ToInt32(bytebuffer, 524))
                    {
                        case 1:
                            currentCMFEntry._iscompressed = true;
                            break;
                        case 2:
                            currentCMFEntry._isencrypted = true;
                            break;
                    }
                    entryList[i] = currentCMFEntry;
                }
                offset += readcount;
            }

            return new ReadOnlyCollection<CMFEntry>(entryList);
        }

        /// <summary>
        /// Return the progressive reader of the CMF Archive.
        /// </summary>
        /// <returns></returns>
        public IReader ExtractAllEntries()
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Archive");

            if (this.myReader != null)
                throw new InvalidOperationException("You can only have one reader per archive. Dispose the old one before getting a new one.");

            this.myReader = new CMFReader(this);
            this.myReader.Disposed += this.MyReader_Disposed;

            return this.myReader;
        }

        private void MyReader_Disposed(object sender, EventArgs e)
        {
            this.myReader = null;
        }

        /// <summary>
        /// Extract the CMF file to the destination folder
        /// </summary>
        /// <param name="outputFolder">Destination folder</param>
        public void ExtractAllEntries(string outputFolder)
        {
            this.ExtractAllEntries(outputFolder, null);
        }

        /// <summary>
        /// Extract the CMF file to the destination folder
        /// </summary>
        /// <param name="outputFolder">Destination folder</param>
        /// <param name="progressChangedCallback">The progress callback handler</param>
        public void ExtractAllEntries(string outputFolder, System.ComponentModel.ProgressChangedEventHandler progressChangedCallback)
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Archive");

            float current = 0F;
            string fullpath;
            using (IReader reader = this.ExtractAllEntries())
                while (reader.MoveToNextEntry())
                {
                    fullpath = Path.Combine(outputFolder, reader.Entry.FileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
                    reader.WriteEntryTo(fullpath);
                    current += 1;
                    progressChangedCallback?.Invoke(this, new System.ComponentModel.ProgressChangedEventArgs(Convert.ToInt32(current * 100 / this.FileCount), null));
                }
        }

        /// <summary>
        /// Decompress the entry to a destination path.
        /// </summary>
        /// <param name="entry">The entry which will be decompressed</param>
        /// <param name="filepath">Destination of the file</param>
        public void ExtractEntry(CMFEntry entry, string filepath)
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Archive");

            using (FileStream fs = File.Create(filepath))
                this.ExtractEntry(entry, fs);
        }

        /// <summary>
        /// Decompress the entry to a stream.
        /// </summary>
        /// <param name="entry">The entry which will be decompressed</param>
        /// <param name="outStream">The output stream</param>
        public void ExtractEntry(CMFEntry entry, Stream outStream)
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Archive");

            if (!outStream.CanWrite)
                throw new InvalidOperationException("The stream should be writable.");

            long entrydataoffset = entry.dataoffset + this.dataoffsetStart;

            this.BaseStream.Seek(entrydataoffset, SeekOrigin.Begin);

            if (!CmfFormat.IsEncryptedFile(entry.FileName) && entry.IsCompressed)
            {
                using (Stream srcStream = new ZlibStream(new EntryStream(this.BaseStream, entrydataoffset, entry.CompressedSize, true), System.IO.Compression.CompressionMode.Decompress, false))
                {
                    srcStream.CopyTo(outStream);
                    outStream.Flush();
                }
            }
            else
            {
                // Let's extract encrypted content as raw data, too.
                // Because I don't know how to decrypt.
                using (Stream srcStream = new EntryStream(this.BaseStream, entrydataoffset, entry.UnpackedSize, true))
                {
                    srcStream.CopyTo(outStream);
                    outStream.Flush();
                }
            }
        }

        /// <summary>
        /// Create the archive editor for the current CMF Archive instance.
        /// </summary>
        /// <returns></returns>
        public IEditor OpenEditor()
        {
            return this.OpenEditor(null);
        }

        /// <summary>
        /// Create the archive editor for the current CMF Archive instance.
        /// </summary>
        /// <param name="temporaryFolder">The directory for temporary files</param>
        /// <returns></returns>
        public IEditor OpenEditor(string temporaryFolder)
        {
            return this.OpenEditor(temporaryFolder, CompressionLevel.Default);
        }

        /// <summary>
        /// Create the archive editor with the given CompressionLevel for the current CMF Archive instance.
        /// </summary>
        /// <param name="compressionLevel">The compression level that the editor will use to compress data</param>
        /// <param name="temporaryFolder">The directory for temporary files</param>
        /// <returns></returns>
        public IEditor OpenEditor(string temporaryFolder, CompressionLevel compressionLevel)
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Archive");

            if (this.myReader != null)
                throw new InvalidOperationException("You can only have one reader per archive. Dispose the old one before getting a new one.");

            if (string.IsNullOrWhiteSpace(temporaryFolder))
                temporaryFolder = Path.GetTempPath();

            this.myEditor = new CMFEditor(this, temporaryFolder, compressionLevel);
            this.myEditor.Disposed += this.myEditor_Disposed;

            return this.myEditor;
        }

        private void myEditor_Disposed(object sender, EventArgs e)
        {
            this.myEditor = null;
        }

        private bool _disposed;
        /// <summary>
        /// Close the CMF archive.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed) return;
            this._disposed = true;

            if (this.myReader != null)
                this.myReader.Dispose();

            if (this.myEditor != null)
                this.myEditor.Dispose();

            if (!this.leaveStreamOpen)
            {
                if (this.binaryReader != null)
                    this.binaryReader.Dispose();
                this.BaseStream.Dispose();
            }
        }
    }
}
