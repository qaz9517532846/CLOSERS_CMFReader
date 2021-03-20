using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leayal.Closers.CMF.Helper
{
    internal static class StringHelper
    {
        internal static int Occurence(this string str, char c)
        {
            int index = str.IndexOf(c),
                result = 0;
            while (index != -1)
            {
                result++;
                index = str.IndexOf(c, index + 1);
            }
            return result;
        }

        internal static string RemoveNullChar(this string str)
        {
            int howmanynull = Occurence(str, '\0');
            if (howmanynull == 0)
                return str;
            StringBuilder sb = new StringBuilder(str.Length - howmanynull);
            for (int i=0;i<str.Length;i++)
                if (str[i] != '\0')
                    sb.Append(str, i, 1);
            return sb.ToString();
        }
    }
}
