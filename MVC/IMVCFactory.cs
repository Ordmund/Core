namespace Core.MVC
{
    public interface IMVCFactory
    {
        TController InstantiateAndBind<TController, TView, TModel>(string prefabName)
            where TController : BaseController<TView, TModel>
            where TView : BaseView
            where TModel : BaseModel;
    }
}