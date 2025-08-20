using System;
using UnityEngine;

namespace Core.Managers.Injectable
{
    public class UnityCallbacksBehaviour : MonoBehaviour, IUnityCallbacksBehaviour
    {
        public event Action OnUpdate = delegate { };
        public event Action OnFixedUpdate = delegate { };

        private void Update()
        {
            OnUpdate.Invoke();
        }
        
        private void FixedUpdate()
        {
            OnFixedUpdate.Invoke();
        }
    }
}