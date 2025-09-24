using System;
using System.Collections.Generic;
using Zenject;

namespace Core.Managers.Injectable
{
    public class TickNotifier : ITickNotifier, ITickable, IFixedTickable, IDisposable
    {
        private readonly List<Action> _onTickActions = new();
        private readonly List<Action> _onFixedTickActions = new();
        
        public void Tick()
        {
            foreach (var onTickAction in _onTickActions)
            {
                onTickAction.Invoke();
            }
        }

        public void FixedTick()
        {
            foreach (var onFixedTickAction in _onFixedTickActions)
            {
                onFixedTickAction.Invoke();
            }
        }
        
        public void SubscribeOnTick(Action action)
        {
            if (action != null)
            {
                _onTickActions.Add(action);
            }
        }

        public void SubscribeOnFixedTick(Action action)
        {
            if (action != null)
            {
                _onFixedTickActions.Add(action);
            }
        }

        public void UnsubscribeFromTick(Action action)
        {
            if (_onTickActions.Contains(action))
            {
                _onTickActions.Remove(action);
            }
        }

        public void UnsubscribeFromFixedTick(Action action)
        {
            if (_onFixedTickActions.Contains(action))
            {
                _onFixedTickActions.Remove(action);
            }
        }

        public void Dispose()
        {
            _onTickActions.Clear();
            _onFixedTickActions.Clear();
        }
    }
}