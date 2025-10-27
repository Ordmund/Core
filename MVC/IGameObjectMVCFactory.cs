using System.Threading.Tasks;
using UnityEngine;

namespace Core.MVC
{
    public interface IGameObjectMVCFactory
    {
        Task<TController> InstantiateAndBindAsync<TController, TView, TModel>(string path = null)
            where TController : BaseController<TView, TModel>
            where TView : BaseView
            where TModel : BaseModel;

        public TController FindObjectAndBind<TController, TView, TModel>()
            where TController : BaseController<TView, TModel>
            where TView : BaseView
            where TModel : BaseModel;

        public TController GetComponentAndBind<TController, TView, TModel>(GameObject gameObject, bool allowSearchInChildren)
            where TController : BaseController<TView, TModel>
            where TView : BaseView
            where TModel : BaseModel;
    }
}