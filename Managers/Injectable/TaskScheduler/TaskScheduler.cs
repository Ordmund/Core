using System.Collections;
using System.Collections.Generic;

namespace Core.Managers.Injectable
{
    public class TaskScheduler : ITaskScheduler
    {
        private readonly IUnityCallbacks _unityCallbacks;
        
        private readonly List<RunningTask> _tasks = new();
        
        public TaskScheduler(IUnityCallbacks unityCallbacks)
        {
            _unityCallbacks = unityCallbacks;
        
            _unityCallbacks.OnUpdate += MoveNext;
        }

        public void Run(Task task, float delay = default)
        {
            if (task != null)
            {
                if (delay > default(float)) 
                    task = new DelayTask(delay).Next(task);

                _tasks.Add(new RunningTask(task));
            }
        }
        
        public void Abort(Task task)
        {
            var result = _tasks.Find(runningTask => runningTask.task == task);
            if (result == null) return;
            
            _tasks.Remove(result);
            result.task.onAbort?.Invoke();
        }

        private void MoveNext()
        {
            if (_tasks.Count == 0)
                return;

            for (var index = 0; index < _tasks.Count; index++)
            {
                var task = _tasks[index];
                if (task.current is { IsComplete: false }) 
                    continue;
                
                var result = task.enumerator.MoveNext();
                if (result)
                {
                    task.current = task.enumerator.Current as YieldInstruction;
                    continue;
                }
                
                task.task.onComplete?.Invoke();
                if (task.task.nextTask != null)
                    _tasks.Add(new RunningTask(task.task.nextTask));

                _tasks.RemoveAt(index);
                index--;
            }
        }

        public void Dispose()
        {
            _unityCallbacks.OnUpdate -= MoveNext;
            
            _tasks.Clear();
        }
        
        private class RunningTask
        {
            public readonly Task task;
            public readonly IEnumerator enumerator;
            public YieldInstruction current;

            public RunningTask(Task task)
            {
                this.task = task;
                enumerator = task.Execute();
            }
        }
    }
}