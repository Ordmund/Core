using System;

namespace Core.Managers.Injectable
{
    public interface ITickNotifier
    {
        public void SubscribeOnTick(Action action);
        public void SubscribeOnFixedTick(Action action);
        public void UnsubscribeFromTick(Action action);
        public void UnsubscribeFromFixedTick(Action action);
    }
}