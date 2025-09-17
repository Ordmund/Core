using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Managers;
using Zenject;

namespace Core.MVC
{
    public class GameObjectMVCFactory : IGameObjectMVCFactory
    {
        private readonly DiContainer _container;
        private readonly IPrefabPathProvider _prefabPathProvider;
        
        public GameObjectMVCFactory(DiContainer container, IPrefabPathProvider prefabPathProvider)
        {
            _container = container;
            _prefabPathProvider = prefabPathProvider;
        }

        public TController InstantiateAndBind<TController, TView, TModel>(string path = null)
            where TController : BaseController<TView, TModel> 
            where TView : BaseView
            where TModel : BaseModel
        {
            path ??= _prefabPathProvider.GetPathByViewType<TView>();
            var viewPrefab = ResourcesManager.Load<TView>(path);
            var view = UnityEngine.Object.Instantiate(viewPrefab);
            var model = GetModel<TModel>();
            var controller = BindAndResolve<TController, TView, TModel>(view, model);

            return controller;
        }
        
        public async Task<TController> InstantiateAndBindAsync<TController, TView, TModel>(string path = null)
            where TController : BaseController<TView, TModel> 
            where TView : BaseView
            where TModel : BaseModel
        {
            path ??= _prefabPathProvider.GetPathByViewType<TView>();
            var viewPrefab = ResourcesManager.Load<TView>(path);
            var task = UnityEngine.Object.InstantiateAsync(viewPrefab);
            var viewsArray = await task;
            var view = viewsArray.FirstOrDefault();
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