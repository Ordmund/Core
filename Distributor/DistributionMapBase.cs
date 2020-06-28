using System;
using System.Collections.Generic;

namespace Core.Managers
{
    public abstract class DistributionMapBase : Dictionary<Type, Type>
    {
        protected void Add<TInterface, TClass>() 
            where TInterface : class, IDistributable
            where TClass : class, TInterface
        {
            var interfaceType = typeof(TInterface);
            var classType = typeof(TClass);

            if (ContainsKey(interfaceType))
                throw new TypeAlreadyMappedException($"Distributable of type {classType} : {interfaceType} is mapped already.");

            base.Add(interfaceType, classType);
        }

        private class TypeAlreadyMappedException : Exception
        {
            public TypeAlreadyMappedException(string message) : base(message)
            {
            }
        }
    }
}