using Core.Managers.Injectable;
using Core.MVC;
using Zenject;

namespace Core.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUnityCallbacks();
            BindTaskScheduler();
            BindMVCFactory();
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

        private void BindMVCFactory()
        {
            Container.Bind<IGameObjectMVCFactory>().To<GameObjectMVCFactory>().AsSingle();
        }
    }
}