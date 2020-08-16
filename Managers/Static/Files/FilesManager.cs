using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Core.Managers.Files
{
    public static class FilesManager
    {
        public static void Save<T>(this T data)
        {
            var type = data.GetType();
            if (!type.IsSerializable)
                throw new Exception($"Type {type.FullName} is not serializable!");

            var json = JsonUtility.ToJson(data);
            var encryptedJson = json.Encrypt();
            var bytes = Encoding.ASCII.GetBytes(encryptedJson);

            File.WriteAllBytes(GetPath(type.Name), bytes);
        }

        public static T Load<T>()
        {
            var typeName = typeof(T).Name;
            var path = GetPath(typeName);
            
            if (File.Exists(path))
            {
                var bytes = File.ReadAllBytes(path);
                var encryptedJson = Encoding.ASCII.GetString(bytes);
                var json = encryptedJson.Decrypt();

                return JsonUtility.FromJson<T>(json);
            }

            return default;
        }

        private static string GetPath(string typeName)
        {
            return Path.Combine(Application.persistentDataPath, typeName);
        }
    }
}