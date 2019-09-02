using System.Linq;
using ColoredLogger;
using ProjectName.Tools;
using UnityEditor;
using UnityEngine;

namespace Core.Managers.ScriptableObjects
{
    public class ScriptableObjectsPathHandler : ScriptableObject
    {
        [SerializeField] private string[] pathsToScriptableObjects;
        [SerializeField] private ScriptableObjectPath[] paths;

        public string GetPath(string objectName)
        {
            var pathModel = paths.FirstOrDefault(model => model.name == objectName);
            if (pathModel != null)
                return pathModel.path;

            $"Path for ScriptableObject {objectName} not found!".Error(LogColor.Crimson, LogsChannel.ScriptableObjects);
            return string.Empty;
        }

        [ContextMenu("Update paths")]
        private void UpdatePaths()
        {
            if (pathsToScriptableObjects == null)
            {
                "Path to ScriptableObjects folder is empty!".Error(LogColor.Crimson, LogsChannel.Editor);
                return;
            }

            var GUIDs = AssetDatabase.FindAssets($"t:{nameof(ScriptableObject)}", pathsToScriptableObjects);
            paths = new ScriptableObjectPath[GUIDs.Length];
            
            for (var assetIndex = 0; assetIndex < GUIDs.Length; assetIndex++)
            {
                var path = AssetDatabase.GUIDToAssetPath(GUIDs[assetIndex]);
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                var conflictingPath = paths.FirstOrDefault(scriptableObjectPath => scriptableObjectPath != null && scriptableObjectPath.name == asset.name);
                if (conflictingPath != null)
                    $"ScriptableObjects path handler already contains path for name [{asset.name}]. Conflicting objects are: {conflictingPath.path} and {path}.".Error(LogColor.Red, LogsChannel.Editor);

                paths[assetIndex] = new ScriptableObjectPath {name = asset.name, path = path};
            }

            "ScriptableObjects paths successfully updated!".Debug(LogColor.Green, LogsChannel.Editor);
        }
    }
}