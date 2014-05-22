#region usings

using System;
using System.Text;
using System.Text.RegularExpressions;

#endregion usings

namespace DDMSense.Extensions
{
    internal static class StringHelperClass
    {
        internal static string[] Split(this string self, string regexDelimiter, bool trimTrailingEmptyStrings)
        {
            string[] splitArray = Regex.Split(self, regexDelimiter);

            if (trimTrailingEmptyStrings)
            {
                if (splitArray.Length > 1)
                {
                    for (int i = splitArray.Length; i > 0; i--)
                    {
                        if (splitArray[i - 1].Length > 0)
                        {
                            if (i < splitArray.Length)
                                Array.Resize(ref splitArray, i);
                            break;
                        }
                    }
                }
            }

            return splitArray;
        }

        /// <summary>
        ///     Returns an empty string in place of a null one.
        /// </summary>
        /// <param name="string"> the string to convert, if null </param>
        /// <returns> an empty string if the string is null, or the string untouched </returns>
        public static string ToNonNullString(this string str)
        {
            if (String.IsNullOrEmpty(str)) return string.Empty;
            return str;
        }
    }
}