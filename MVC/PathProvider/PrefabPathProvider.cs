using System;
using Core.Managers.ScriptableObjects;
using Zenject;

namespace Core.MVC
{
    public class PrefabPathProvider : IPrefabPathProvider, IInitializable, IDisposable
    {
        private PrefabsPathsScriptableObject _prefabsPathsScriptableObject;
        
        public void Initialize()
        {
            _prefabsPathsScriptableObject = ScriptableObjectsManager.Load<PrefabsPathsScriptableObject>();
        }
        
        public string GetPathByViewType<T>() where T : BaseView
        {
            return _prefabsPathsScriptableObject.GetPath<T>();
        }

        public void Dispose()
        {
            ScriptableObjectsManager.Unload(_prefabsPathsScriptableObject);
        }
    }
}