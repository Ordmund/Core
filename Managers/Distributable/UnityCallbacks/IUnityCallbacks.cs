using System;

namespace Core.Managers
{
    public interface IUnityCallbacks : IDistributable
    {
        event Action OnUpdate;
    }
}