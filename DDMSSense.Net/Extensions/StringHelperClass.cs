#region usings

using System;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace DDMSSense.Extensions
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
    }
}