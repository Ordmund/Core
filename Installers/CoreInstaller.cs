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
            BindGameObjectMVCFactory();
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

        private void BindGameObjectMVCFactory()
        {
            Container.Bind<IGameObjectMVCFactory>().To<GameObjectMVCFactory>().AsSingle();
            Container.Bind<IPrefabPathProvider>().To<PrefabPathProvider>().AsSingle();
        }
    }
}