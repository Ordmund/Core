using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Core.MVC
{
    public class GameObjectMVCFactory : IGameObjectMVCFactory
    {
        private readonly DiContainer _container;
        private readonly IPrefabsPathProvider _prefabsPathProvider;
        
        public GameObjectMVCFactory(DiContainer container, IPrefabsPathProvider prefabsPathProvider)
        {
            _container = container;
            _prefabsPathProvider = prefabsPathProvider;
        }
        
        public async Task<TController> InstantiateAndBindAsync<TController, TView, TModel>(string path = null)
            where TController : BaseController<TView, TModel> 
            where TView : BaseView
            where TModel : BaseModel
        {
            path ??= _prefabsPathProvider.GetPathByViewType<TView>();
            var asyncOperationHandle = Addressables.InstantiateAsync(path);
            var gameObject = await asyncOperationHandle.Task;
            var view = gameObject.GetComponent<TView>();
            var model = GetModel<TModel>();
            var controller = BindAndResolve<TController, TView, TModel>(view, model);

            return controller;
        }

        public TController FindObjectAndBind<TController, TView, TModel>()
            where TController : BaseController<TView, TModel> 
            where TView : BaseView
            where TModel : BaseModel
        {
            var view = UnityEngine.Object.FindAnyObjectByType<TView>();
            var model = GetModel<TModel>();
            var controller = BindAndResolve<TController, TView, TModel>(view, model);

            return controller;
        }

        private TController BindAndResolve<TController, TView, TModel>(TView view, TModel model)
        {
            _container.Bind<TController>().AsSingle().WithArguments(view, model).NonLazy();
            var controller = _container.Resolve<TController>();

            return controller;
        }

        private static TModel GetModel<TModel>() where TModel : BaseModel
        {
            return Activator.CreateInstance<TModel>();   
        }
    }
}