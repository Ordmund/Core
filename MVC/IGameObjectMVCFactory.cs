using System.Threading.Tasks;

namespace Core.MVC
{
    public interface IGameObjectMVCFactory
    {
        TController InstantiateAndBind<TController, TView, TModel>(string path = null)
            where TController : BaseController<TView, TModel>
            where TView : BaseView
            where TModel : BaseModel;
        
        Task<TController> InstantiateAndBindAsync<TController, TView, TModel>(string path = null)
            where TController : BaseController<TView, TModel>
            where TView : BaseView
            where TModel : BaseModel;

        public TController FindObjectAndBind<TController, TView, TModel>()
            where TController : BaseController<TView, TModel>
            where TView : BaseView
            where TModel : BaseModel;
    }
}