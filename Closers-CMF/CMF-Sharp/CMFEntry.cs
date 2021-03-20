namespace Leayal.Closers.CMF
{
    /// <summary>
    /// Provide information of the entry.
    /// </summary>
    public class CMFEntry
    {
        internal bool _iscompressed;
        /// <summary>
        /// Determine if the entry's content is compressed or not.
        /// </summary>
        public bool IsCompressed => this._iscompressed;
        internal bool _isencrypted;
        /// <summary>
        /// Determine if the entry's content is encrypted or not.
        /// </summary>
        public bool IsEncrypted => this._isencrypted;
        internal string _filename;
        /// <summary>
        /// Entry's filename
        /// </summary>
        public string FileName => this._filename;
        internal long _compressedsize;
        /// <summary>
        /// Get the size of entry's compressed content inside CMF file.
        /// </summary>
        public long CompressedSize => this._compressedsize;
        internal long _unpackedsize;
        /// <summary>
        /// Get the real size of entry's content before compressing.
        /// </summary>
        public long UnpackedSize => this._unpackedsize;

        internal long dataoffset;
        internal int headeroffset;

        internal CMFEntry()
        {
            this._iscompressed = false;
            this._isencrypted = false;
        }
    }
}
