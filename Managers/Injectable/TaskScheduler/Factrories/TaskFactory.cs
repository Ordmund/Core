using Zenject;

namespace Core.Managers.Injectable
{
    public class TaskFactory :  ITaskFactory
    {
        private readonly DiContainer _container;

        public TaskFactory(DiContainer container)
        {
            _container = container;
        }

        public TTask InstantiateAndBind<TTask>() where TTask : Task
        {
            _container.Bind<TTask>().AsSingle().NonLazy();
            var task = _container.Resolve<TTask>();

            return task;
        }
    }
}