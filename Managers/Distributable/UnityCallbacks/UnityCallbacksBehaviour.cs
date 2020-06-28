using System;
using UnityEngine;

namespace Core.Managers
{
    public class UnityCallbacksBehaviour : MonoBehaviour
    {
        public event Action OnUpdate = delegate { };

        private void Update()
        {
            OnUpdate.Invoke();
        }
    }
}