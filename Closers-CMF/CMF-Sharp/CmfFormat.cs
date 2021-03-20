using System.IO;

namespace Leayal.Closers.CMF
{
    internal static class CmfFormat
    {
        public const int EntryKey1 = -1399622946;
        public const int EntryKey2 = -2073448703;
        public const int EntryKey3 = -600218993;

        public const int FileHeaderSize = 528;
        public const int FileHeaderNameSize = 512;

        internal static bool IsEncryptedFile(string filename)
        {
            int extIndex = filename.LastIndexOf('.');
            if (extIndex > -1)
            {
                string ext = filename.Substring(extIndex + 1);
                if (string.IsNullOrEmpty(ext))
                    return false;
                else
                {
                    if (string.Equals(ext, "lua", System.StringComparison.OrdinalIgnoreCase))
                        return true;
                    else if (string.Equals(ext, "tet", System.StringComparison.OrdinalIgnoreCase))
                        return true;
                    else if (string.Equals(ext, "xet", System.StringComparison.OrdinalIgnoreCase))
                        return true;
                    else if (string.Equals(ext, "fx", System.StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }
    }
}
