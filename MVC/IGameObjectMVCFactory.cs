namespace Core.MVC
{
    public interface IGameObjectMVCFactory
    {
        TController InstantiateAndBind<TController, TView, TModel>(string prefabName)
            where TController : BaseController<TView, TModel>
            where TView : BaseView
            where TModel : BaseModel;
    }
}