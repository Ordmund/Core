using ColoredLogger;
using UnityEditor;
using UnityEngine;

namespace Core.Managers.ScriptableObjects
{
    public class ScriptableObjectsCreator : EditorWindow
    {
        private DefaultAsset _targetFolder;
        private string[] _scriptableObjects;
        private int _selectedIndex;
        
        [MenuItem("Tools/ScriptableObjects/Creator")]
        private static void OpenWindow()
        {
            GetWindow<ScriptableObjectsCreator>();
        }

        private void GetScriptableObjects()
        {
            var scriptableObjects = AssetDatabase.FindAssets($"t:{nameof(ScriptableObject)}");
            scriptableObjects.Log();
            
            
        }

        private void OnGUI()
        {
            _targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Select Folder", _targetFolder, typeof(DefaultAsset), false);
            _selectedIndex = EditorGUILayout.Popup("ScriptableObject", _selectedIndex, _scriptableObjects);

            if (GUILayout.Button("Create"))
            {
                
            }
        }
    }
}