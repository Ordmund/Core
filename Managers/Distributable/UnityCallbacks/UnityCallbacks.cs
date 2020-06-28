using System;
using UnityEngine;

namespace Core.Managers
{
    public class UnityCallbacks : MonoBehaviour, IUnityCallbacks
    {
        private UnityCallbacksBehaviour _behaviour;

        public event Action OnUpdate;

        public int InitializationGeneration => 0;

        public void Initialize(Distributor distributor)
        {
            var behaviourGameObject = new GameObject(nameof(UnityCallbacks));
            _behaviour = behaviourGameObject.AddComponent<UnityCallbacksBehaviour>();

            _behaviour.OnUpdate += Update;
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        public void Restart(Distributor distributor)
        {
            //Ignored
        }

        public void Dispose()
        {
            _behaviour.OnUpdate -= Update;

            OnUpdate = null;

            Destroy(_behaviour.gameObject);
        }
    }
}