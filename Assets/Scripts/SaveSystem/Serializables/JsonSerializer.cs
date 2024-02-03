using UnityEngine;

namespace Mechanics.SaveSystem.Serializables
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public string Serialize(T data)
        {
            return JsonUtility.ToJson(data);
        }

        public T Deserialize(string serializedData)
        {
            return JsonUtility.FromJson<T>(serializedData);
        }
    }
}