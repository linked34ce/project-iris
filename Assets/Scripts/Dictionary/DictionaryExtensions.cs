using System.Collections.Generic;

public static class DictionaryExtensions
{
    /// <summary>
    /// Get the value associated with the key,
    /// or set a default value if the key does not exist,
    /// and return the default value.
    /// </summary>
    public static TValue GetValueOrDefault<TKey, TValue>(
        this Dictionary<TKey, TValue> dic,
        TKey key,
        TValue defaultValue = default
    ) => dic.TryGetValue(key, out TValue result) ? result : defaultValue;
}
