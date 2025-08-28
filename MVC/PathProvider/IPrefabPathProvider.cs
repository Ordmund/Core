namespace Core.MVC
{
    public interface IPrefabPathProvider
    {
        string GetPathByViewType<T>() where T : BaseView;
    }
}