#region usings

using System.Collections.Generic;

#endregion usings

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
            return ret;
        }
    }
}