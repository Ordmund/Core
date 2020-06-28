using System.Collections;

namespace Core.Managers
{
    public class DelayTask : Task
    {
        private readonly float _delay;

        public DelayTask(float delay)
        {
            _delay = delay;
        }

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(_delay);
        }
    }
}