using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers.ScriptableObjects
{
    public static class ScriptableObjectsManager
    {
        private const string PathHandlerPath = "ScriptableObjects/Handlers/PathHandler.asset"; //TODO set by reflections?
        
        private static List<ScriptableObject> _scriptableObjects;
        private static ScriptableObjectsPathHandler _pathHandler;

        private static ScriptableObjectsPathHandler PathHandler => _pathHandler != null ? _pathHandler : _pathHandler = ResourcesManager.Load<ScriptableObjectsPathHandler>(PathHandlerPath);

        public static T Load<T>() where T : ScriptableObject
        {
            throw new NotImplementedException();
        }

        public static void Unload(ScriptableObject scriptableObject)
        {
            ResourcesManager.Unload(scriptableObject);
        }

        public static void Unload<T>()
        {
            throw new NotImplementedException();
        }

        public static void UnloadAll()
        {
            _scriptableObjects.ForEach(ResourcesManager.Unload);
            _scriptableObjects.Clear();
        }
    }
}