namespace Core.Managers
{
    public interface ITaskScheduler : IDistributable
    {
        void Run(Task task, float delay = default);
        void Abort(Task task);
    }
}