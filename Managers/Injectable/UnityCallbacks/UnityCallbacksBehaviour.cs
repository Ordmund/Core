using System;
using UnityEngine;

namespace Core.Managers.Injectable
{
    public class UnityCallbacksBehaviour : MonoBehaviour, IUnityCallbacksBehaviour
    {
        public event Action OnUpdate = delegate { };

        private void Update()
        {
            OnUpdate.Invoke();
        }
    }
}