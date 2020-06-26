using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Middle_Manager_API
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.BaseType == (typeof(T))));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type.ImplementedInterfaces.FirstOrDefault(), lifetime));
        }
    }
}
