using UnityEngine;

namespace Core.Managers.Injectable
{
    public class WaitForNextFrame : YieldInstruction
    {
        private readonly int _nextFrame = Time.frameCount + 1;

        public override bool IsComplete => Time.frameCount >= _nextFrame;
    }
}