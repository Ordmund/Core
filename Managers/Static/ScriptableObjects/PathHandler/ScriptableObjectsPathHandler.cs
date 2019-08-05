using System.Linq;
using ColoredLogger;
using ProjectName.Tools;
using UnityEditor;
using UnityEngine;

namespace Core.Managers.ScriptableObjects
{
    public class ScriptableObjectsPathHandler : ScriptableObject
    {
        [SerializeField] private ScriptableObjectPath[] paths;

        public string GetPath<T>() where T : ScriptableObject
        {
            var typeName = typeof(T).Name;
            var pathModel = paths.FirstOrDefault(model => model.name == typeName);
            if (pathModel != null)
                return pathModel.path;

            $"Path for type {typeName} not found!".Error(LogColor.Crimson, LogsChannel.ScriptableObjects);
            return string.Empty;
        }

        [ContextMenu("Update paths")]
        private void UpdatePaths()
        {
            var GUIDs = AssetDatabase.FindAssets($"t:{nameof(ScriptableObject)}");

            paths = new ScriptableObjectPath[GUIDs.Length];

            for (var assetIndex = 0; assetIndex < GUIDs.Length; assetIndex++)
            {
                var path = AssetDatabase.GUIDToAssetPath(GUIDs[assetIndex]);
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                paths[assetIndex] = new ScriptableObjectPath {name = asset.name, path = path};
            }
        }
    }
}