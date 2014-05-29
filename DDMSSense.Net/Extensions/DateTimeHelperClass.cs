using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDMSense.Extensions
{
    internal static class DateTimeHelperClass
    {
        public static DateTime? EnsureKind(this DateTime? date)
        {
            //If no value or the value already has a DateTimeKind, return the original date
            if (!date.HasValue || !date.Value.Kind.Equals(DateTimeKind.Unspecified))
                return date;

            //Return a DateTime? with UTC specified as the DateTimeKind
            return DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);
        }
        
        public static string ToDDMSDateTimeString(this DateTime? date)
        {
            if (!date.HasValue)
                return null;

            var ret = date.EnsureKind();

            //If no time then formay yyyy-MM-dd, else format the full time.
            if (ret.Value.TimeOfDay.TotalMilliseconds.Equals(0))
                return ret.Value.ToString("yyyy-MM-dd");
            else
                return ret.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
        }
    }

}
