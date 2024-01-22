using System;

namespace Core.Managers.Injectable
{
    public interface IController
    {
        event Action<IController> OnStop;
        
        void Run();
        void Stop();
    }
}