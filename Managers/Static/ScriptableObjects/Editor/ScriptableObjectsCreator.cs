using System.Text.RegularExpressions;
using ColoredLogger;
using ProjectName.Tools;
using UnityEditor;
using UnityEngine;

namespace Core.Managers.ScriptableObjects
{
    public class ScriptableObjectsCreator : EditorWindow
    {
        private DefaultAsset _targetFolder;
        private string[] _scriptableObjects;
        private int _selectedIndex;

        private readonly string[] _scriptableObjectsFolder = {"Assets/Resources/ScriptableObjects"};
        
        [MenuItem("Tools/ScriptableObjects/Creator")]
        private static void OpenWindow()
        {
            var window = GetWindow<ScriptableObjectsCreator>();
            if (window)
                window.GetScriptableObjects();
        }

        private void GetScriptableObjects()
        {
            const string ScriptableObjectNamePattern = @"(?<=\/)\w+(?=\.asset$)";
            
            var GUIDs = AssetDatabase.FindAssets($"t:{nameof(ScriptableObject)}", _scriptableObjectsFolder);
            _scriptableObjects = new string[GUIDs.Length];
            for (var index = 0; index < GUIDs.Length; index++)
            {
                var GUID = GUIDs[index];
                var path = AssetDatabase.GUIDToAssetPath(GUID);
                if (Regex.IsMatch(path, ScriptableObjectNamePattern))
                    _scriptableObjects[index] = Regex.Match(path, ScriptableObjectNamePattern).ToString();
                else
                    $"Regex not found ScriptableObject name in path {path}.".Error(LogColor.Tomato, LogsChannel.Editor);
            }
        }

        private void OnGUI()
        {
            _targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Select Folder", _targetFolder, typeof(DefaultAsset), false);
            _selectedIndex = EditorGUILayout.Popup("ScriptableObject", _selectedIndex, _scriptableObjects);

            if (GUILayout.Button("Create"))
            {
                if (_scriptableObjects.Length == 0)
                {
                    "Not found any available ScriptableObject to create!".Debug(LogColor.Teal, LogsChannel.Editor);
                    return;
                }

                var assetPath = AssetDatabase.GetAssetPath(_targetFolder);
                if (string.IsNullOrEmpty(assetPath))
                {
                    "Folder not selected!".Error(LogColor.DarkOrange, LogsChannel.Editor);
                    return;
                }
                
                var asset = CreateInstance(_scriptableObjects[_selectedIndex]);
                AssetDatabase.CreateAsset(asset, assetPath);
                
                $"{_scriptableObjects[_selectedIndex]} successfully created!".Debug(LogColor.Green, LogsChannel.Editor);
            }
        }
    }
}