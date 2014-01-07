#region usings

using System.Collections.Generic;

#endregion

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
    }
}