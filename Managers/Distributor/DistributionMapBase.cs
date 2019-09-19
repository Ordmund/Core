using System;
using System.Collections.Generic;

namespace Core.Managers
{
    public abstract class DistributionMapBase
    {
        private readonly Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        protected void Add<TInterface, TClass>() 
            where TInterface : class, IDistributable
            where TClass : class, TInterface
        {
            var interfaceType = typeof(TInterface);
            var classType = typeof(TClass);

            if (_map.ContainsKey(interfaceType))
                throw new TypeAlreadyMappedException($"Distributable of type {classType} : {interfaceType} is mapped already.");

            _map.Add(interfaceType, classType);
        }

        /// <summary>
        /// Returns a map of distributable implementations where a value is a class that implements key's interface.
        /// </summary>
        /// <returns>Dictionary of distributable implementations types</returns>
        public Dictionary<Type, Type> GetMap()
        {
            return new Dictionary<Type, Type>(_map);
        }

        private class TypeAlreadyMappedException : Exception
        {
            public TypeAlreadyMappedException(string message) : base(message)
            {
            }
        }
    }
}