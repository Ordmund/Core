using System;
using UnityEngine;
using Zenject;

namespace Core.Managers.Injectable
{
    public class UnityCallbacks : MonoBehaviour, IUnityCallbacks, IInitializable, IDisposable
    {
        private UnityCallbacksBehaviour _behaviour;

        public event Action OnUpdate;
        
        public void Initialize()
        {
            InstantiateAndSubscribeOnUpdate();
        }

        private void InstantiateAndSubscribeOnUpdate()
        {
            var behaviourGameObject = new GameObject(nameof(UnityCallbacks));
            _behaviour = behaviourGameObject.AddComponent<UnityCallbacksBehaviour>();

            _behaviour.OnUpdate += Update;
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        public void Dispose()
        {
            _behaviour.OnUpdate -= Update;

            OnUpdate = null;

            Destroy(_behaviour.gameObject);
        }
    }
}