using UnityEngine;

namespace Core.Managers.Injectable
{
    public class WaitForSeconds : YieldInstruction
    {
        private float _seconds;

        public WaitForSeconds(float seconds)
        {
            _seconds = seconds;
        }

        public override bool IsComplete
        {
            get
            {
                _seconds -= Time.deltaTime;
                return _seconds <= 0f;
            }
        }
    }
}