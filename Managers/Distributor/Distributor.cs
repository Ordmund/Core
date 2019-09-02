using System;
using System.Collections.Generic;

namespace Core.Managers
{
    public class Distributor
    {
        private readonly Dictionary<Type, IDistributable> _instances = new Dictionary<Type, IDistributable>();

        public void Initialize(IEnumerable<Type> instancesTypes)
        {
            var previousInstances = _instances;
            _instances.Clear();
            
            foreach (var instanceType in instancesTypes)
            {
                if (previousInstances.ContainsKey(instanceType))
                {
                    _instances.Add(instanceType, previousInstances[instanceType]);
                    previousInstances.Remove(instanceType);
                }
                else
                {
                    var instance = Activator.CreateInstance(instanceType) as IDistributable;
                    _instances.Add(instanceType, instance);
                }
            }

            foreach (var previousInstance in previousInstances.Values) 
                previousInstance.Dispose();
        }

        public T Get<T>() where T : IDistributable
        {
            var type = typeof(T);
            if (_instances.ContainsKey(type))
                return (T) _instances[type];

            throw new InstanceNotFoundException($"Distributor do not contains instance of {type.FullName} class!");
        }

        private class InstanceNotFoundException : Exception
        {
            public InstanceNotFoundException(string message) : base(message)
            {
            }
        }
    }
}