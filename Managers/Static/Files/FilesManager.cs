using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Core.Managers.Files
{
    public static class FilesManager
    {
        public static void Save<T>(this T data) where T : new()
        {
            var type = typeof(T);
            if(!type.IsSerializable)
                throw new Exception($"Type {type.FullName} is not serializable!");

            var json = JsonUtility.ToJson(data);
            var encryptedJson = json.Encrypt();
            var bytes = Encoding.ASCII.GetBytes(encryptedJson);

            File.WriteAllBytes(GetPath<T>(), bytes);
        }

        public static T Load<T>() where T : new()
        {
            if (File.Exists(GetPath<T>()))
            {
                var bytes = File.ReadAllBytes(GetPath<T>());
                var encryptedJson = Encoding.ASCII.GetString(bytes);
                var json = encryptedJson.Decrypt();

                return JsonUtility.FromJson<T>(json);
            }

            return new T();
        }

        private static string GetPath<T>() where T : new()
        {
            return Path.Combine(Application.persistentDataPath, typeof(T).Name);
        }
    }
}