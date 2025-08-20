using System;

namespace Core.Managers.Injectable
{
    public interface IUnityCallbacksBehaviour
    {
        event Action OnUpdate;
        event Action OnFixedUpdate;
    }
}