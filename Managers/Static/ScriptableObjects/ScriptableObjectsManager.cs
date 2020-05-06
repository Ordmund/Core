using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Core.Constants.DependenciesProvider;

namespace Core.Managers.ScriptableObjects
{
    public static class ScriptableObjectsManager
    {
        private static readonly List<ScriptableObject> _scriptableObjects = new List<ScriptableObject>();
        private static ScriptableObjectsPathHandler _pathHandler;

        private static ScriptableObjectsPathHandler PathHandler => _pathHandler != null ? _pathHandler : _pathHandler = ResourcesManager.Load<ScriptableObjectsPathHandler>(PathHandlerPath);

        public static TScriptableObject Load<TScriptableObject>(string name = null) where TScriptableObject : ScriptableObject
        {
            if (name == null)
                name = typeof(TScriptableObject).Name;
            
            var searchResult = _scriptableObjects.FirstOrDefault(loadedScriptableObject => loadedScriptableObject.name == name);
            if (searchResult != null)
                return searchResult as TScriptableObject;

            var path = PathHandler.GetPath(name);
            var scriptableObject = ResourcesManager.Load<TScriptableObject>(path);

            if (scriptableObject != null)
            {
                _scriptableObjects.Add(scriptableObject);
                return scriptableObject;
            }

            Debug.LogError($"ScriptableObject with name [{name}] and type {typeof(TScriptableObject)} not found!");
            return null;
        }

        public static void Unload(ScriptableObject scriptableObject)
        {
            if (_scriptableObjects.Contains(scriptableObject))
                _scriptableObjects.Remove(scriptableObject);

            ResourcesManager.Unload(scriptableObject);
        }

        public static void Unload(string name)
        {
            var searchResult = _scriptableObjects.FirstOrDefault(loadedScriptableObject => loadedScriptableObject.name == name);
            if (searchResult == null)
                return;

            _scriptableObjects.Remove(searchResult);
            ResourcesManager.Unload(searchResult);
        }

        public static void UnloadAll()
        {
            _scriptableObjects.ForEach(ResourcesManager.Unload);
            _scriptableObjects.Clear();
        }
    }
}