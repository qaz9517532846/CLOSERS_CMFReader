using SharpCompress.Compressors.Deflate;
using System;
using System.IO;
using System.Text;

namespace Leayal.Closers.CMF
{
    /// <summary>
    /// Provide methods to edit the CMF file content. (Not thread-safe)
    /// </summary>
    public interface IEditor : IDisposable
    {
        /// <summary>
        /// Get the path to the directory which contains temporary files for writing CMF operation.
        /// </summary>
        string TemporaryFolder { get; }
        /// <summary>
        /// Determine if the <seealso cref="IEditor"/> is writing to output file.
        /// </summary>
        bool IsSaving { get; }
        /// <summary>
        /// Get the compression level of the editor.
        /// </summary>
        CompressionLevel CompressionLevel { get; }

        /// <summary>
        /// Set new data for the entry at given index.
        /// </summary>
        /// <param name="entryIndex">The index of the entry</param>
        /// <param name="data">The new data of the entry</param>
        /// <returns></returns>
        bool SetData(int entryIndex, byte[] data);

        /// <summary>
        /// Set new data for the entry which matches the given entry path. Return True if the entry is found, otherwise False.
        /// </summary>
        /// <param name="entryPath">The path point to an entry</param>
        /// <param name="data">The new data of the entry</param>
        /// <returns></returns>
        bool SetData(string entryPath, byte[] data);

        /// <summary>
        /// Set new data for the entry which matches the given entry. Return True if the entry is found, otherwise False.
        /// </summary>
        /// <param name="entry">The entry info</param>
        /// <param name="data">The new data of the entry</param>
        /// <returns></returns>
        bool SetData(CMFEntry entry, byte[] data);

        /// <summary>
        /// Set new data from a stream for the entry at given index.
        /// </summary>
        /// <param name="entryIndex">The index of the entry</param>
        /// <param name="data">The stream of the source data. The stream must be opening until <seealso cref="Save"/> is called.</param>
        /// <returns></returns>
        bool SetDataSource(int entryIndex, Stream data);

        /// <summary>
        /// Set new data from a stream for the entry which matches the given entry path. Return True if the entry is found, otherwise False.
        /// </summary>
        /// <param name="entryPath">The path point to an entry.</param>
        /// <param name="data">The stream of the source data. The stream must be opening until <seealso cref="Save"/> is called.</param>
        /// <returns></returns>
        bool SetDataSource(string entryPath, Stream data);

        /// <summary>
        /// Set new data from a stream for the entry which matches the given entry. Return True if the entry is found, otherwise False.
        /// </summary>
        /// <param name="entry">The entry info</param>
        /// <param name="data">The stream of the source data. The stream must be opening until <seealso cref="Save"/> is called.</param>
        /// <returns></returns>
        bool SetDataSource(CMFEntry entry, Stream data);

        /// <summary>
        /// Set new string data from a stream for the entry at given index.
        /// </summary>
        /// <param name="entryIndex">The index of the entry</param>
        /// <param name="data">The new string data</param>
        /// <returns></returns>
        bool SetString(int entryIndex, string data);

        /// <summary>
        /// Set new string data from a stream for the entry which matches the given entry path. Return True if the entry is found, otherwise False.
        /// </summary>
        /// <param name="entryPath">The path point to an entry</param>
        /// <param name="data">The new string data</param>
        /// <returns></returns>
        bool SetString(string entryPath, string data);

        /// <summary>
        /// Set new string data from a stream for the entry which matches the given entry. Return True if the entry is found, otherwise False.
        /// </summary>
        /// <param name="entry">The entry info</param>
        /// <param name="data">The new string data</param>
        /// <returns></returns>
        bool SetString(CMFEntry entry, string data);

        /// <summary>
        /// Set new string data from a stream for the entry at given index.
        /// </summary>
        /// <param name="entryIndex">The index of the entry</param>
        /// <param name="data">The new string data</param>
        /// <param name="encoding">The encoding used to encode the new string data</param>
        /// <returns></returns>
        bool SetString(int entryIndex, string data, Encoding encoding);

        /// <summary>
        /// Set new string data from a stream for the entry which matches the given entry path. Return True if the entry is found, otherwise False.
        /// </summary>
        /// <param name="entryPath">The path point to an entry</param>
        /// <param name="data">The new string data</param>
        /// <param name="encoding">The encoding used to encode the new string data</param>
        /// <returns></returns>
        bool SetString(string entryPath, string data, Encoding encoding);

        /// <summary>
        /// Set new string data from a stream for the entry which matches the given entry. Return True if the entry is found, otherwise False.
        /// </summary>
        /// <param name="entry">The entry info</param>
        /// <param name="data">The new string data</param>
        /// <param name="encoding">The encoding used to encode the new string data</param>
        /// <returns></returns>
        bool SetString(CMFEntry entry, string data, Encoding encoding);

        /// <summary>
        /// Save the modified data to the current archive.
        /// </summary>
        void Save();

        /// <summary>
        /// Write the whole CMF archive with modified datas to a new file.
        /// </summary>
        /// <param name="filepath">The path to the file</param>
        void WriteTo(string filepath);

        /// <summary>
        /// Write the whole CMF archive with modified datas to a stream.
        /// </summary>
        /// <param name="outStream">The destination stream</param>
        void WriteTo(Stream outStream);
    }
}
