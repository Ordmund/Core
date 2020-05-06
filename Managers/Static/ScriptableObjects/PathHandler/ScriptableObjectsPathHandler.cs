using System.Linq;
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

            Debug.LogError($"Path for ScriptableObject {objectName} not found!");
            return string.Empty;
        }

        [ContextMenu("Update paths")]
        private void UpdatePaths()
        {
            if (pathsToScriptableObjects == null)
            {
                Debug.LogError("Path to ScriptableObjects folder is empty!");
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
                    Debug.LogError($"ScriptableObjects path handler already contains path for name [{asset.name}]. Conflicting objects are: {conflictingPath.path} and {path}.");

                path = path.Replace("Assets/Resources/", string.Empty).Replace(".asset", string.Empty);
                paths[assetIndex] = new ScriptableObjectPath {name = asset.name, path = path};
            }

            Debug.Log("<color=green>ScriptableObjects paths successfully updated!</color>");
        }
    }
}