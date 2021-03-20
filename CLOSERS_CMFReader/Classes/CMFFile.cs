using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Leayal.Closers.CMF;

namespace CLOSERS_CMFReader.Classes
{
    /// <summary>
    /// Wrapper for CMFArchive
    /// </summary>
    public class CMFFile : IDisposable
    {
        private CMFArchive archive;
        public CMFArchive Archive => this.archive;
        public string Filename { get; }
        public CMFFile(string filename)
        {
            if (!System.IO.File.Exists(filename))
                throw new System.IO.FileNotFoundException("Could not find the file", filename);
            this.Filename = filename;
        }

        private bool _isreadonly;
        public bool IsReadonly => this._isreadonly;

        public void BeginRead()
        {
            System.IO.FileStream fs;
            try
            {
                fs = System.IO.File.Open(this.Filename, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read);
                this._isreadonly = false;
            }
            catch (UnauthorizedAccessException)
            {
                fs = System.IO.File.OpenRead(this.Filename);
                this._isreadonly = true;
            }
            catch (System.IO.IOException)
            {
                fs = System.IO.File.OpenRead(this.Filename);
                this._isreadonly = true;
            }
            this.archive = CMFArchive.Read(fs, false);
            this.OnReady();
        }

        /// <summary>
        /// Return the number of entries in the CMF Archive.
        /// </summary>
        public int FileCount => this.archive.FileCount;
        /// <summary>
        /// Return the list of entry in the CMF archive.
        /// </summary>
        public ReadOnlyCollection<CMFEntry> Entries => this.archive.Entries;
        /// <summary>
        /// Return the <seealso cref="CMFEntry"/> at the specific index.
        /// </summary>
        /// <param name="index">The index of the entry</param>
        /// <returns></returns>
        public CMFEntry this[int index] => this.Entries[index];
        /// <summary>
        /// Return the <seealso cref="CMFEntry"/> which match the given full-path inside the archive. Return null if no entry matches.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public CMFEntry this[string path] => this.archive[path];

        public event EventHandler Ready;
        protected void OnReady()
        {
            this.Ready?.Invoke(this.archive, EventArgs.Empty);
        }

        public event EventHandler Closed;
        protected void OnClosed()
        {
            this.Closed?.Invoke(this.archive, EventArgs.Empty);
        }

        /// <summary>
        /// Return the progressive reader of the CMF Archive.
        /// </summary>
        /// <returns></returns>
        public IReader ExtractAllEntries() => this.archive.ExtractAllEntries();

        /// <summary>
        /// Extract the CMF file to the destination folder
        /// </summary>
        /// <param name="outputFolder">Destination folder</param>
        public void ExtractAllEntries(string outputFolder)
        {
            this.archive.ExtractAllEntries(outputFolder, null);
        }

        /// <summary>
        /// Extract the CMF file to the destination folder
        /// </summary>
        /// <param name="outputFolder">Destination folder</param>
        /// <param name="progressChangedCallback">The progress callback handler</param>
        public void ExtractAllEntries(string outputFolder, System.ComponentModel.ProgressChangedEventHandler progressChangedCallback)
        {
            this.archive.ExtractAllEntries(outputFolder, progressChangedCallback);
        }

        /// <summary>
        /// Decompress the entry to a destination path.
        /// </summary>
        /// <param name="entry">The entry which will be decompressed</param>
        /// <param name="filepath">Destination of the file</param>
        public void ExtractEntry(CMFEntry entry, string filepath)
        {
            this.archive.ExtractEntry(entry, filepath);
        }

        /// <summary>
        /// Decompress the entry to a stream.
        /// </summary>
        /// <param name="entry">The entry which will be decompressed</param>
        /// <param name="outStream">The output stream</param>
        public void ExtractEntry(CMFEntry entry, System.IO.Stream outStream)
        {
            this.archive.ExtractEntry(entry, outStream);
        }

        private bool _disposed;
        /// <summary>
        /// Close the CMF archive.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed) return;
            this._disposed = true;
            this.archive.Dispose();
            this.OnClosed();
        }
    }
}
