namespace Core.MVC
{
    public abstract class BaseController<TView, TModel>
    {
        protected TView View { get; }
        protected TModel Model { get; }

        protected BaseController(TView view, TModel model)
        {
            View = view;
            Model = model;
        }
    }
}