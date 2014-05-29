#region usings

using DDMSense.Util;
using System;
using System.Globalization;
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

        /// <summary>
        ///     DoD Discovery Metadata Specification
        ///     v4.1+ * Table C7.T11. dates Category / Element *
        ///     Asserts that a date format is one of the 5 types accepted by DDMS.
        ///     Recommended practice is that date be specified in one of the following formats:
        ///     YYYY
        ///		YYYY-MM
        ///		YYYY-MM-DD
        ///		YYYY-MM-DDThhTZD
        ///		YYYY-MM-DDThh:mmTZD
        ///		YYYY-MM-DDThh:mm:ssTZD
        /// 	YYYY-MM-DDThh:mm:ss.sTZD
        /// 	Where:
        /// 	YYYY	0000 through current year
        /// 	MM	01 through 12  (month)
        /// 	DD	01 through 31  (day)
        /// 	hh	00 through 23  (hour)
        /// 	mm	00 through 59  (minute)
        /// 	ss	00 through 59  (second)
        /// 	.s	.0 through 999 (fractional second)
        /// 	TZD  = time zone designator (Z or +hh:mm or -hh:mm)
        /// </summary>
        /// <param name="dateString"> the date in string format </param>
        public static DateTime? ToDDMSNullableDateTime(this string dateString)
        {
            try
            {
                //Valid date formats across all DDMS versions
                //We'll let other logic ensure proper formats for each version
                string[] validFormats = {   "yyyy",
										    "yyyy-MM",
										    "yyyy-MM-dd",
										    "yyyy-MM-ddTHHK",
										    "yyyy-MM-ddTHH:mmK",
										    "yyyy-MM-ddTHH:mm:ssK",
										    "yyyy-MM-ddTHH:mm:ss.fK",
										    "yyyy-MM-ddTHH:mm:ss.ffK",
										    "yyyy-MM-ddTHH:mm:ss.fffK"
									    };

                //Parse the date
                var date = DateTime.ParseExact(dateString, validFormats, null, DateTimeStyles.RoundtripKind);

                //Ensure that we have a DateTimeKind.
                if(date.Kind.Equals(DateTimeKind.Unspecified))
                    date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

                return date;
            }
            catch (System.FormatException)
            {
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}