using System;
using System.Collections;

namespace Core.Managers
{
    public abstract class Task
    {
        private Distributor _distributor;
        
        public Action onComplete;
        public Action onAbort;
        public Task nextTask;

        protected IDistributable Get<T>() where T : class, IDistributable
        {
            if (_distributor == null)
                _distributor = DistributionProvider.GetDistributor();

            return _distributor.Get<T>();
        }

        public abstract IEnumerator Execute();

        public Task Next(Task task)
        {
            nextTask = task;
            return task;
        }

        public Task OnComplete(Action action)
        {
            onComplete = action;
            return this;
        }
        
        public Task OnAbort(Action action)
        {
            onAbort = action;
            return this;
        }
    }
}