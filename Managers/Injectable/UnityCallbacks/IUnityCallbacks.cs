using System;

namespace Core.Managers.Injectable
{
    public interface IUnityCallbacks
    {
        event Action OnUpdate;
    }
}