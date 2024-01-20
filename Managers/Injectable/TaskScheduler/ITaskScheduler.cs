namespace Core.Managers.Injectable
{
    public interface ITaskScheduler
    {
        void Run(Task task, float delay = default);
        void Abort(Task task);
    }
}