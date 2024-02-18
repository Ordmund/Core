using System;
using ColoredLogger;
using Core.Managers;

namespace Core.MVC
{
    public class MVCFactory : IMVCFactory
    {
        public TController InstantiateAndBind<TController, TView, TModel>(string prefabName)
            where TController : BaseController<TView, TModel> 
            where TView : BaseView
            where TModel : BaseModel
        {
            var view = ResourcesManager.Load<TView>(prefabName); //TODO looks like messed up logic>? Should move somewher>?
            UnityEngine.Object.Instantiate(view); //TODO who will destroy and how?>
            
            var model = Activator.CreateInstance<TModel>();
            var controller = (TController)Activator.CreateInstance(typeof(TController), view, model);
            controller.Log();

            return controller;
        }
    }
}