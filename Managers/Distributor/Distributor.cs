using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Managers
{
    public class Distributor
    {
        private readonly Dictionary<Type, IDistributable> _instances = new Dictionary<Type, IDistributable>();

        #region Instances Configuration
        
        public void UpdateInstances(Dictionary<Type, Type> newMap)
        {
            var previousInstances = new Dictionary<Type, IDistributable>(_instances);
            _instances.Clear();
            
            var instancesToRestart = GetInstancesToReload(previousInstances, newMap);
            var instancesToInitialize = GetInstancesToInitialize(instancesToRestart, newMap);
            var instancesToDispose = GetInstancesToDispose(previousInstances);

            foreach (var distributable in instancesToDispose) 
                distributable.Dispose();

            foreach (var distributable in instancesToRestart) 
                distributable.Restart(this);
            
            foreach (var distributable in instancesToInitialize)
                distributable.Initialize(this);
        }
        
        private static List<IDistributable> GetInstancesToReload(Dictionary<Type, IDistributable> previousInstances, Dictionary<Type, Type> newMap)
        {
            var instancesToReload = new List<IDistributable>();
            foreach (var instance in from instance in previousInstances.Values
                let sameInstance = newMap.Values.FirstOrDefault(distributable => distributable == instance.GetType())
                where sameInstance != null && !instancesToReload.Contains(instance)
                select instance) instancesToReload.Add(instance);

            return instancesToReload;
        }

        private IEnumerable<IDistributable> GetInstancesToInitialize(IReadOnlyCollection<IDistributable> instancesToReload, Dictionary<Type, Type> newMap)
        {
            var instancesToInitialize = new List<IDistributable>();
            foreach (var instanceType in newMap)
            {
                var interfaceType = instanceType.Key;
                var classType = instanceType.Value;

                var activeInstance = instancesToReload.FirstOrDefault(distributable => distributable.GetType() == classType);
                if (activeInstance != null) 
                    _instances.Add(interfaceType, activeInstance);
                else
                {
                    var createdInstance = _instances.Values.FirstOrDefault(instance => instance.GetType() == classType);
                    if (createdInstance != null) 
                        _instances.Add(interfaceType, createdInstance);
                    else
                    {
                        var newInstance = Activator.CreateInstance(classType) as IDistributable;
                        
                        instancesToInitialize.Add(newInstance);
                        _instances.Add(interfaceType, newInstance);
                    }
                }
            }

            return instancesToInitialize.OrderBy(instance => instance.InitializationGeneration);
        }

        private IEnumerable<IDistributable> GetInstancesToDispose(Dictionary<Type, IDistributable> previousInstances)
        {
            return previousInstances.Values.Where(previousInstance => _instances.Values.All(instance => instance != previousInstance)).ToList();
        }

        #endregion
        
        public T Get<T>() where T : class, IDistributable
        {
            var type = typeof(T);
            if (!type.IsInterface)
                throw new NotInterfaceTypeException($"Requested type {typeof(T)} is not an interface.");

            if (!_instances.ContainsKey(type))
                throw new InstanceNotFoundException($"Distributor do not contains an instance of {type.FullName} class!");
            
            return (T) _instances[type];
        }
        
        private class NotInterfaceTypeException : Exception
        {
            public NotInterfaceTypeException(string message) : base(message)
            {
            }
        }

        private class InstanceNotFoundException : Exception
        {
            public InstanceNotFoundException(string message) : base(message)
            {
            }
        }
    }
}