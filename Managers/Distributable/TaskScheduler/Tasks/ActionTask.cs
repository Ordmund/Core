using System;
using System.Collections;

namespace Core.Managers
{
    public class ActionTask : Task
    {
        private readonly Action _action;
            
        public ActionTask(Action action)
        {
            _action = action;
        }

        public override IEnumerator Execute()
        {
            _action?.Invoke();
                
            yield break;
        }
    }
}