namespace Core.Managers.Injectable
{
    public interface ITaskFactory
    {
        TTask InstantiateAndBind<TTask>() where TTask : Task;
    }
}