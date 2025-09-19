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
            BindGameObjectMVCFactory();
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
            Container.BindInterfacesAndSelfTo<PrefabsPathProvider>().AsSingle();
        }
    }
}