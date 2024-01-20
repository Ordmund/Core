using System;
using System.Collections;

namespace Core.Managers.Injectable
{
    public abstract class Task
    {
        public Action onComplete;
        public Action onAbort;
        public Task nextTask;

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