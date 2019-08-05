using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers
{
    public static class ScriptableObjectsManager
    {
        private static List<ScriptableObject> _scriptableObjects;

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