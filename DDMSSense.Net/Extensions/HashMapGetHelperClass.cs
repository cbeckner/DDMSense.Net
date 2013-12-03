namespace DDMSSense.Extensions
{
    internal static class HashMapGetHelperClass
    {
        internal static TValue GetValueOrNull<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue ret;
            dictionary.TryGetValue(key, out ret);
            return ret;
        }
    }
}