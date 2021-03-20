using System;
using System.IO;

namespace Leayal.Closers.CMF
{
    /// <summary>
    /// Provide progressive interaction with archive's entries.
    /// </summary>
    public interface IReader : IDisposable
    {
        /// <summary>
        /// Move to the next entry. Return True if on success, otherwise False if there is no entry left.
        /// </summary>
        /// <returns></returns>
        bool MoveToNextEntry();
        /// <summary>
        /// Open a stream to read entry.
        /// </summary>
        /// <returns></returns>
        Stream OpenEntryStream();
        /// <summary>
        /// Write the entry's content to a stream.
        /// </summary>
        /// <param name="outStream">Destination stream</param>
        void WriteEntryTo(Stream outStream);
        /// <summary>
        /// Write the entry's content to a file.
        /// </summary>
        /// <param name="filepath">Destination file</param>
        void WriteEntryTo(string filepath);
        /// <summary>
        /// Current entry info.
        /// </summary>
        CMFEntry Entry { get; }
    }
}
