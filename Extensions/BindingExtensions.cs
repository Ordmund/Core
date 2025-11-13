using System.Linq;
using Zenject;

namespace Core.Extensions
{
    public static class BindingExtensions
    {
        public static void BindAllFromAssemblyAsTransient<TContract>(this DiContainer container)
        {
            var contractType = typeof(TContract);
            var concreteClassesTypes = contractType.Assembly.GetTypes().Where(type => contractType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);

            foreach (var type in concreteClassesTypes)
            {
                container.Bind(contractType).To(type).AsTransient();
            }
        }
    }
}