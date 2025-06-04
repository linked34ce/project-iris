using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> :
    Dictionary<TKey, TValue>,
    ISerializationCallbackReceiver
{
    [Serializable]
    public class Pair
    {
        public TKey Key = default;
        public TValue Value = default;

        /// <summary>
        /// Pair
        /// </summary>
        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    [SerializeField] private List<Pair> _list = null;

    /// <summary>
    /// OnAfterDeserialize
    /// </summary>
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Clear();
        foreach (var pair in _list)
        {
            if (ContainsKey(pair.Key))
            {
                continue;
            }
            Add(pair.Key, pair.Value);
        }
    }

    /// <summary>
    /// OnBeforeSerialize
    /// </summary>
    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        // Nothing to do
    }
}
