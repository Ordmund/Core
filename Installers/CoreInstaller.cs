using Core.Managers.Injectable;
using Zenject;

namespace Core.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUnityCallbacks>().To<UnityCallbacks>().AsSingle();
            Container.Bind<ITaskScheduler>().To<TaskScheduler>().AsSingle();
        }
    }
}