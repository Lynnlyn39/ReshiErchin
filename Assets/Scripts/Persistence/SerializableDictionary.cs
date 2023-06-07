using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{    
    public List<TKey> keys = new List<TKey>();
    public List<TValue> values = new List<TValue>();    

    public void OnBeforeSerialize()
    {
        Debug.Log("SerializableDictionary::OnAfterDeserialize");
        keys.Clear();
        values.Clear();
        foreach (var kvp in this)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        Debug.Log("SerializableDictionary::OnBeforeSerialize");
        Clear();
        if (keys.Count != values.Count)
        {
            Debug.LogError("Dictionary serialization error: different amount of Keys & values.");
        }
        for (int i = 0; i < keys.Count; i++)
        {
            Add(keys[i], values[i]);
        }
    }
}
