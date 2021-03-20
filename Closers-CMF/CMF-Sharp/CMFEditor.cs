using SharpCompress.Compressors.Deflate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Leayal.Closers.CMF
{
    class CMFEditor : IEditor
    {
        private Dictionary<CMFEntry, FileStream> datadictionary;
        private CMFArchive myArchive;
        private bool _issaving;
        public bool IsSaving => this._issaving;
        private byte[] myultimatebuffer;

        internal CMFEditor(CMFArchive archive, string tempfolder, CompressionLevel compression)
        {
            this._issaving = false;
            this.myArchive = archive;
            this.CompressionLevel = compression;
            this.TemporaryFolder = tempfolder;
            this.myultimatebuffer = new byte[4096];
            this.datadictionary = new Dictionary<CMFEntry, FileStream>();
        }

        public CompressionLevel CompressionLevel { get; }

        public string TemporaryFolder { get; }

        private bool SetEntryData(CMFEntry entry, object data, long length)
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Editor");

            if (this._issaving)
                throw new InvalidOperationException("The Editor is writing data. Adding more data is not possible."); // Although it is possible. But I'm lazy.

            if (entry == null || this.myArchive.Entries.IndexOf(entry) == -1)
                return false;
            else
            {
                if (length == -1)
                {
                    if (data is Stream stream)
                    {
                        FileStream fs = SpawnTempFile();
                        if (!CmfFormat.IsEncryptedFile(entry.FileName) && entry.IsCompressed)
                            using (ZlibStream zlibstream = new ZlibStream(fs, System.IO.Compression.CompressionMode.Compress, this.CompressionLevel, true, Encoding.UTF8))
                                CopyStream(stream, zlibstream, ref this.myultimatebuffer);
                        else
                            CopyStream(stream, fs, ref this.myultimatebuffer);
                        if (fs.Length > entry.CompressedSize)
                        {
                            fs.Dispose();
                            throw new InvalidDataException($"The new data of '{entry.FileName}' size is bigger than the original one.");
                        }
                        else
                        {
                            if (this.datadictionary.ContainsKey(entry))
                                this.datadictionary[entry].Dispose();
                            this.datadictionary[entry] = fs;
                            fs.Flush();
                        }
                    }
                    else if (data is byte[] dataArray)
                    {
                        if (!CmfFormat.IsEncryptedFile(entry.FileName) && entry.IsCompressed)
                        {
                            FileStream fs = SpawnTempFile();
                            using (ZlibStream zlibstream = new ZlibStream(fs, System.IO.Compression.CompressionMode.Compress, this.CompressionLevel, true, Encoding.UTF8))
                                zlibstream.Write(dataArray, 0, dataArray.Length);

                            if (fs.Length > entry.CompressedSize)
                            {
                                fs.Dispose();
                                throw new InvalidDataException($"The new data of '{entry.FileName}' size is bigger than the original one.");
                            }
                            else
                            {
                                if (this.datadictionary.ContainsKey(entry))
                                    this.datadictionary[entry].Dispose();
                                this.datadictionary[entry] = fs;
                                fs.Flush();
                            }
                        }
                        else
                        {
                            if (dataArray.Length > entry.CompressedSize)
                            {
                                throw new InvalidDataException($"The new data of '{entry.FileName}' size is bigger than the original one.");
                            }
                            else
                            {
                                FileStream fs = SpawnTempFile();
                                fs.Write(dataArray, 0, dataArray.Length);
                                if (this.datadictionary.ContainsKey(entry))
                                    this.datadictionary[entry].Dispose();
                                this.datadictionary[entry] = fs;
                                fs.Flush();
                            }
                        }
                    }
                }
                else
                {
                    if (data is Stream stream)
                    {
                        FileStream fs = SpawnTempFile();
                        if (length > 0)
                        {
                            if (!CmfFormat.IsEncryptedFile(entry.FileName) && entry.IsCompressed)
                                using (ZlibStream zlibstream = new ZlibStream(fs, System.IO.Compression.CompressionMode.Compress, this.CompressionLevel, true, Encoding.UTF8))
                                    CopyStream(stream, zlibstream, ref this.myultimatebuffer, length);
                            else
                                CopyStream(stream, fs, ref this.myultimatebuffer, length);
                        }
                        if (fs.Length > entry.CompressedSize)
                        {
                            fs.Dispose();
                            throw new InvalidDataException($"The new data of '{entry.FileName}' size is bigger than the original one.");
                        }
                        else
                        {
                            if (this.datadictionary.ContainsKey(entry))
                                this.datadictionary[entry].Dispose();
                            this.datadictionary[entry] = fs;
                            fs.Flush();
                        }
                    }
                    else if (data is byte[] dataArray)
                    {
                        if (!CmfFormat.IsEncryptedFile(entry.FileName) && entry.IsCompressed)
                        {
                            FileStream fs = SpawnTempFile();
                            using (ZlibStream zlibstream = new ZlibStream(fs, System.IO.Compression.CompressionMode.Compress, this.CompressionLevel, true, Encoding.UTF8))
                                zlibstream.Write(dataArray, 0, dataArray.Length);

                            if (fs.Length > entry.CompressedSize)
                            {
                                fs.Dispose();
                                throw new InvalidDataException($"The new data of '{entry.FileName}' size is bigger than the original one.");
                            }
                            else
                            {
                                if (this.datadictionary.ContainsKey(entry))
                                    this.datadictionary[entry].Dispose();
                                this.datadictionary[entry] = fs;
                                fs.Flush();
                            }
                        }
                        else
                        {
                            if (dataArray.Length > entry.CompressedSize)
                            {
                                throw new InvalidDataException($"The new data of '{entry.FileName}' size is bigger than the original one.");
                            }
                            else
                            {
                                FileStream fs = SpawnTempFile();
                                fs.Write(dataArray, 0, dataArray.Length);
                                if (this.datadictionary.ContainsKey(entry))
                                    this.datadictionary[entry].Dispose();
                                this.datadictionary[entry] = fs;
                                fs.Flush();
                            }
                        }
                    }
                }
                return true;
            }
        }

        public bool SetData(int entryIndex, byte[] data)
        {
            if ((0 <= entryIndex) && (entryIndex < this.myArchive.Entries.Count))
                return this.SetData(this.myArchive[entryIndex], data);
            else
                return false;
        }

        public bool SetData(string entryPath, byte[] data)
        {
            return this.SetData(this.myArchive[entryPath], data);
        }

        public bool SetData(CMFEntry entry, byte[] data)
        {
            return this.SetEntryData(entry, data, data.LongLength);
        }

        public bool SetDataSource(int entryIndex, Stream data)
        {
            if ((0 <= entryIndex) && (entryIndex < this.myArchive.Entries.Count))
                return this.SetDataSource(this.myArchive[entryIndex], data);
            else
                return false;
        }

        public bool SetDataSource(string entryPath, Stream data)
        {
            return this.SetDataSource(this.myArchive[entryPath], data);
        }

        public bool SetDataSource(CMFEntry entry, Stream data)
        {
            if (!data.CanRead)
                throw new InvalidOperationException("The data's stream should be readable.");
            return this.SetEntryData(entry, data, -1);
        }

        public bool SetDataSource(int entryIndex, Stream data, long length)
        {
            if ((0 <= entryIndex) && (entryIndex < this.myArchive.Entries.Count))
                return this.SetDataSource(this.myArchive[entryIndex], data, length);
            else
                return false;
        }

        public bool SetDataSource(string entryPath, Stream data, long length)
        {
            return this.SetDataSource(this.myArchive[entryPath], data, length);
        }

        public bool SetDataSource(CMFEntry entry, Stream data, long length)
        {
            if (!data.CanRead)
                throw new InvalidOperationException("The data's stream should be readable.");
            return this.SetEntryData(entry, data, length);
        }

        public bool SetString(int entryIndex, string data)
        {
            if ((0 <= entryIndex) && (entryIndex < this.myArchive.Entries.Count))
                return this.SetString(this.myArchive[entryIndex], data);
            else
                return false;
        }

        public bool SetString(string entryPath, string data)
        {
            return this.SetString(this.myArchive[entryPath], data);
        }

        public bool SetString(CMFEntry entry, string data)
        {
            return this.SetString(entry, data, Encoding.UTF8);
        }

        public bool SetString(int entryIndex, string data, Encoding encoding)
        {
            if ((0 <= entryIndex) && (entryIndex < this.myArchive.Entries.Count))
                return this.SetString(this.myArchive[entryIndex], data, encoding);
            else
                return false;
        }

        public bool SetString(string entryPath, string data, Encoding encoding)
        {
            return this.SetString(this.myArchive[entryPath], data, encoding);
        }

        public bool SetString(CMFEntry entry, string data, Encoding encoding)
        {
            return this.SetEntryData(entry, encoding.GetBytes(data), -1);
        }

        public void Save()
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Editor");

            if (this._issaving)
                return;

            if (!this.myArchive.BaseStream.CanWrite)
                throw new InvalidOperationException("The archive is opened with read-only stream. Use SaveAs() instead or re-open the file with read/write access.");

            if (this.datadictionary.Count == 0)
                return;

            this._issaving = true;

            long paddingsize;
            KeyValuePair<CMFEntry, FileStream> entrydata;
            while (this.datadictionary.Count > 0)
            {
                // Not efficient
                entrydata = this.datadictionary.First();
                if (this.myArchive.Entries.IndexOf(entrydata.Key) > -1)
                {
                    this.myArchive.BaseStream.Seek(entrydata.Key.dataoffset, SeekOrigin.Begin);
                    CopyStream(entrydata.Value, this.myArchive.BaseStream, ref this.myultimatebuffer);

                    if (entrydata.Value.Length < entrydata.Key.CompressedSize)
                    {
                        paddingsize = entrydata.Key.CompressedSize - entrydata.Value.Length;
                        for (int paddingCount = 0; paddingCount < paddingsize; paddingCount++)
                            this.myArchive.BaseStream.WriteByte(0);
                    }
                }
                entrydata.Value.Dispose();
                this.datadictionary.Remove(entrydata.Key);
            }

            this.myArchive.BaseStream.Flush();

            this._issaving = false;
        }

        public void WriteTo(string filepath)
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Editor");

            if (this._issaving)
                return;
            
            if (this.myArchive.BaseStream is FileStream tfs && string.Equals(tfs.Name, Path.GetFullPath(filepath), StringComparison.OrdinalIgnoreCase))
                this.Save();
            else
            {
                using (Stream fs = File.Create(filepath))
                    this.WriteTo(fs);
            }
        }

        public void WriteTo(Stream outStream)
        {
            if (this._disposed)
                throw new System.ObjectDisposedException("Editor");

            if (this._issaving)
                return;

            if (datadictionary.Count == 0)
                return;

            this._issaving = true;

            // Copy header
            this.myArchive.BaseStream.Seek(0, SeekOrigin.Begin);
            using (EntryStream stream = new EntryStream(this.myArchive.BaseStream, 0, this.myArchive.dataoffsetStart, true))
                stream.CopyTo(outStream);

            // Write entry's content
            CMFEntry entry;
            FileStream data;
            long paddingsize;

            for (int i = 0; i < this.myArchive.Entries.Count; i++)
            {
                entry = this.myArchive.Entries[i];
                if (this.datadictionary.ContainsKey(entry))
                {
                    data = this.datadictionary[entry];
                    CopyStream(data, this.myArchive.BaseStream, ref this.myultimatebuffer);

                    if (data.Length < entry.CompressedSize)
                    {
                        paddingsize = entry.CompressedSize - data.Length;
                        for (int paddingCount = 0; paddingCount < paddingsize; paddingCount++)
                            this.myArchive.BaseStream.WriteByte(0);
                    }
                    this.datadictionary[entry].Dispose();
                    this.datadictionary.Remove(entry);
                }
                else
                {
                    this.myArchive.BaseStream.Seek(entry.dataoffset + this.myArchive.dataoffsetStart, SeekOrigin.Begin);
                    using (EntryStream stream = new EntryStream(this.myArchive.BaseStream, entry.dataoffset, entry.CompressedSize, true))
                        CopyStream(stream, outStream, ref this.myultimatebuffer);
                }
            }

            outStream.Flush();

            this._issaving = false;
        }

        private static void CopyStream(Stream inStream, Stream outStrema, ref byte[] buffer)
        {
            int readCount = inStream.Read(buffer, 0, buffer.Length);
            while (readCount>0)
            {
                outStrema.Write(buffer, 0, readCount);
                readCount = inStream.Read(buffer, 0, buffer.Length);
            }
        }

        private static void CopyStream(Stream inStream, Stream outStrema, ref byte[] buffer, long length)
        {
            if (length == 0) return;
            int readCount;
            if (length < buffer.Length)
                readCount = inStream.Read(buffer, 0, (int)length);
            else
                readCount = inStream.Read(buffer, 0, buffer.Length);
            while (length > 0 && readCount > 0)
            {
                length -= readCount;
                outStrema.Write(buffer, 0, readCount);
                if (length <= 0)
                    break;
                if (length < buffer.Length)
                    readCount = inStream.Read(buffer, 0, (int)length);
                else
                    readCount = inStream.Read(buffer, 0, buffer.Length);
            }
        }

        // Why "spawn" though???
        private FileStream SpawnTempFile()
        {
            return File.Create(this.TryFileName(), 4096, FileOptions.DeleteOnClose);
        }

        private string TryFileName()
        {
            string path = Path.Combine(this.TemporaryFolder, "cmf-data-" + Guid.NewGuid().ToString());
            if (File.Exists(path))
                return TryFileName();
            else
                return path;
        }

        private bool _disposed;
        public void Dispose()
        {
            if (this._disposed) return;
            this._disposed = true;
            this.datadictionary.Clear();
            this.datadictionary = null;
            this.Disposed?.Invoke(this, EventArgs.Empty);
        }

        internal event EventHandler Disposed;
    }
}
