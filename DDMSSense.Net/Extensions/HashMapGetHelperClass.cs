#region usings

using System.Collections.Generic;

#endregion usings

using System.Linq;

namespace DDMSense.Extensions
{
    internal static class HashMapGetHelperClass
    {
        internal static TValue GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue ret;
            dictionary.TryGetValue(key, out ret);
            return ret;
        }

        internal static string GetValueOrEmpty<TKey>(this IDictionary<TKey, string> dictionary, TKey key)
        {
            string ret = string.Empty;
            dictionary.TryGetValue(key, out ret);
            return ret.ToNonNullString();
        }

        public static int GetOrderIndependentHashCode<T>(this IEnumerable<T> source)
        {
            int hash = 0;
            foreach (T element in source.OrderBy(x => x, Comparer<T>.Default))
            {
                hash += element.GetHashCode();
            }
            return hash;
        }
    }
}