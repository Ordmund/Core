using System;
using System.Collections.Generic;

namespace Core.Managers.Injectable
{
    public class ControllerRunner : IControllerRunner, IDisposable
    {
        private readonly List<IController> _runningController = new();
        
        public TControllerType CreateAndRun<TControllerType>() where TControllerType : IController, new()
        {
            var controller = CreateNewInstance<TControllerType>();
            
            SubscribeOnStop(controller);
            RunController(controller);

            return controller;
        }

        private static TControllerType CreateNewInstance<TControllerType>() where TControllerType : IController, new()
        {
            return new TControllerType();
        }

        private void SubscribeOnStop(IController controller)
        {
            controller.OnStop += OnControllerStopped;
        }

        private void RunController(IController controller)
        {
            _runningController.Add(controller);
            
            controller.Run();
        }

        private void OnControllerStopped(IController controller)
        {
            if (_runningController.Contains(controller))
                _runningController.Remove(controller);
        }

        public void Dispose()
        {
            foreach (var controller in _runningController)
            {
                controller.OnStop -= OnControllerStopped;
                
                controller.Stop();
            }

            _runningController.Clear();
        }
    }
}