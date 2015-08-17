using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets
{
    public class PlayerPrefsSerializer
    {
        public static BinaryFormatter bf = new BinaryFormatter();
        // serializableObject is any struct or class marked with [Serializable]
        public static void Save(string prefKey, object serializableObject)
        {
            var memoryStream = new MemoryStream();
            bf.Serialize(memoryStream, serializableObject);
            string tmp = Convert.ToBase64String(memoryStream.ToArray());
            PlayerPrefs.SetString(prefKey, tmp);
        }

        public static T Load<T>(string prefKey)
        {
            if (!PlayerPrefs.HasKey(prefKey))
                return default(T);

            string serializedData = PlayerPrefs.GetString(prefKey);
            var dataStream = new MemoryStream(Convert.FromBase64String(serializedData));

            var deserializedObject = (T)bf.Deserialize(dataStream);

            return deserializedObject;
        }
    }
}