using Leayal.Closers.CMF.Helper;
using SharpCompress.Compressors.Deflate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Leayal.Closers.CMF
{
    class CMFReader : IReader
    {
        private long dataoffsetStart;
        private IEnumerator<CMFEntry> entryWalker;
        private CMFArchive sourceArchive;
        private Stream currentStream;

        internal CMFReader(CMFArchive archive)
        {
            this.sourceArchive = archive;
            this.dataoffsetStart = archive.dataoffsetStart;
            if (archive.entrylist == null)
                this.entryWalker = new EntriesWalker(archive);
            else
                this.entryWalker = archive.Entries.GetEnumerator();
        }

        public CMFEntry Entry
        {
            get
            {
                if (this._disposed)
                    throw new System.ObjectDisposedException("Reader");
                return this.entryWalker.Current;
            }
        }

        private bool _disposed;
        public void Dispose()
        {
            if (this._disposed) return;
            this._disposed = true;
            if (this.currentStream != null)
            {
                this.currentStream.Dispose();
                this.currentStream = null;
            }
            this.entryWalker.Dispose();
            this.Disposed?.Invoke(this, System.EventArgs.Empty);
        }

        internal event System.EventHandler Disposed;

        public bool MoveToNextEntry()
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Reader");

            bool result = this.entryWalker.MoveNext();
            if (result && this.currentStream != null)
            {
                this.currentStream.Dispose();
                this.currentStream = null;
            }
            return result;
        }

        public Stream OpenEntryStream()
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Reader");

            long entrydataoffset = this.Entry.dataoffset + this.dataoffsetStart;
            this.sourceArchive.BaseStream.Seek(entrydataoffset, SeekOrigin.Begin);
            if (!CmfFormat.IsEncryptedFile(this.Entry.FileName) && this.Entry.IsCompressed)
            {
                this.currentStream = new ZlibStream(new EntryStream(this.sourceArchive.BaseStream, entrydataoffset, Entry.CompressedSize, true), System.IO.Compression.CompressionMode.Decompress, false);
            }
            else
            {
                // Let's extract encrypted content as raw data, too.
                // Because I don't know how to decrypt.
                this.currentStream = new EntryStream(this.sourceArchive.BaseStream, entrydataoffset, Entry.UnpackedSize, true);
            }
            return this.currentStream;
        }

        public void WriteEntryTo(Stream outStream)
        {
            using (Stream entryStream = this.OpenEntryStream())
                entryStream.CopyTo(outStream, 4096);
            outStream.Flush();
        }

        public void WriteEntryTo(string filepath)
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Reader");
            using (FileStream fs = File.Create(filepath))
                this.WriteEntryTo(fs);
        }
    }

    class EntriesWalker : IEnumerator<CMFEntry>
    {
        private CMFArchive sourceArchive;
        private int currentCount;
        private long currentPos;
        private byte[] buffer;
        private BinaryReader br;

        internal EntriesWalker(CMFArchive archive)
        {
            this.myEntry = null;
            this.sourceArchive = archive;
            this.currentCount = 0;
            this.buffer = new byte[CmfFormat.FileHeaderSize];
            this.currentPos = this.sourceArchive.headeroffsetStart;
            this.br = new BinaryReader(this.sourceArchive.BaseStream);
        }

        private CMFEntry myEntry;
        public CMFEntry Current => this.myEntry;

        object IEnumerator.Current => throw new System.NotImplementedException();

        public void Dispose()
        {
            // Do nothing. Don't even dispose "this.br" because it will close the source archive's basestream.
        }

        public bool MoveNext()
        {
            if (this.currentCount < this.sourceArchive.FileCount)
            {
                if (this.myEntry == null)
                    this.myEntry = new CMFEntry();
                this.sourceArchive.BaseStream.Seek(this.currentPos, SeekOrigin.Begin);
                
                int readcount = this.br.Read(this.buffer, 0, this.buffer.Length);
                if (readcount == this.buffer.Length)
                {
                    this.myEntry.headeroffset = (int)this.currentPos;
                    // Decode the buffer.
                    CmfHelper.Decode(ref this.buffer);

                    // First 512 bytes is the filename
                    string tmp_filename = Encoding.ASCII.GetString(this.buffer, 0, CmfFormat.FileHeaderNameSize);

                    // This doesn't look good.
                    int indexofNull = tmp_filename.IndexOf("\0\0");
                    if (tmp_filename.IndexOf("\0\0") == -1)
                    {
                        indexofNull = tmp_filename.LastIndexOf('\0');
                        tmp_filename = Encoding.ASCII.GetString(this.buffer, 0, indexofNull);
                    }
                    else
                        this.myEntry._filename = Encoding.Unicode.GetString(this.buffer, 0, indexofNull + 1);

                    this.myEntry._filename = this.myEntry._filename.RemoveNullChar();

                    // Next is 4 bytes for the unpacked size (aka original file)
                    this.myEntry._unpackedsize = BitConverter.ToInt32(this.buffer, 512);

                    // Next is another 4 bytes for compressedsize
                    this.myEntry._compressedsize = BitConverter.ToInt32(this.buffer, 516);

                    // Next is another 4 bytes for "DataOffset"
                    this.myEntry.dataoffset = BitConverter.ToInt32(this.buffer, 520);

                    // if (str == "FX" || str == "LUA" || str == "TET" || str == "XET")

                    // Last is the flag, determine if the file is compressed (with Zlib) or encrypted or nothing special.
                    switch (BitConverter.ToInt32(this.buffer, 524))
                    {
                        case 1:
                            this.myEntry._iscompressed = true;
                            break;
                        case 2:
                            this.myEntry._isencrypted = true;
                            break;
                    }
                }

                this.currentPos += readcount;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            this.currentCount = -1;
            this.currentPos = this.sourceArchive.headeroffsetStart;
            this.myEntry = null;
        }
    }
}
