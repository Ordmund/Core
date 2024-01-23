using Core.Managers.Injectable;
using Zenject;

namespace Core.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUnityCallbacks();
            BindTaskScheduler();
        }

        private void BindTaskScheduler()
        {
            Container.Bind<ITaskScheduler>().To<TaskScheduler>().AsSingle();
        }

        private void BindUnityCallbacks()
        {
            Container.Bind<IUnityCallbacksBehaviour>()
                .To<UnityCallbacksBehaviour>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();
        }
    }
}