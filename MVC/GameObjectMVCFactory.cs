using System;
using Core.Managers;
using Zenject;

namespace Core.MVC
{
    public class GameObjectMVCFactory : IGameObjectMVCFactory
    {
        private DiContainer _container;
        
        public GameObjectMVCFactory(DiContainer container)
        {
            _container = container;
        }

        public TController InstantiateAndBind<TController, TView, TModel>(string path)
            where TController : BaseController<TView, TModel> 
            where TView : BaseView
            where TModel : BaseModel
        {
            var viewPrefab = ResourcesManager.Load<TView>(path);
            var view = UnityEngine.Object.Instantiate(viewPrefab);
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