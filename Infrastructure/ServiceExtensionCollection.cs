using ChatApp.Web.DataAccess;
using ChatApp.Web.Model.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChatApp.Web.Infrastructure
{
    public static class ServiceExtensionCollection
    {
        public static void AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            services.AddScoped<IDbContext, DatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Repository Configuration
            var repositoryTypes = assembly.GetTypes()
                .Where(type => !type.IsAbstract && type.GetInterfaces()
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<>)));

            // filter out RepositoryBase<>
            var nonBaseRepos = repositoryTypes.Where(t => t != typeof(RepositoryBase<>));

            foreach (var repositoryType in nonBaseRepos)
            {
                string name = repositoryType.Name;
                var interfaces = repositoryType.GetInterfaces()
                    .Where(@interface => @interface.Name.EndsWith("Repository") && @interface.Name.Contains(repositoryType.Name))
                    .ToList();

                if (interfaces.Count != 1)
                {
                    throw new InvalidOperationException($"Repository '{repositoryType.Name}' must implement only one interface that implements IRepositoryBase<T>.");
                }

                services.AddScoped(interfaces[0], repositoryType);
            }

        }
        public static void AddServices(this IServiceCollection services, Assembly assembly)
        {
            var serviceTypes = assembly.GetTypes()
                .Where(type => !type.IsAbstract && type.GetInterfaces()
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IService<>)));

            // filter out RepositoryBase<>
            var nonServiceTypes = serviceTypes.Where(t => t != typeof(ServiceBase<>));

            foreach (var serviceType in nonServiceTypes)
            {
                string name = serviceType.Name;
                var interfaces = serviceType.GetInterfaces()
                    .Where(@interface => @interface.Name.EndsWith("Service") && @interface.Name.Contains(serviceType.Name))
                    .ToList();

                if (interfaces.Count != 1)
                {
                    throw new InvalidOperationException($"Service '{serviceType.Name}' must implement only one interface that implements IService<T>.");
                }

                services.AddScoped(interfaces[0], serviceType);
            }
        }
        public static void AddRepositroyDependingInterface(this IServiceCollection services, Assembly assembly)
        {
            services.AddScoped<IDbContext, DatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Get interfaces
            var interfaces = assembly.GetTypes()
              .Where(type =>type.IsInterface && type.Name.Contains("Repository") && !type.Name.Equals("IRepository`1"))
              .ToList();
            foreach(Type typeInf in interfaces)
            {
                //find repositor
                string name = typeInf.Name;
                string interfaceWithoutPrefix = name.Substring(1);
                List<Type> repositoryClass = assembly.GetTypes()
                .Where(type => !type.IsInterface &&
                               !type.IsAbstract &&
                               type.Name.Equals(interfaceWithoutPrefix) &&
                               !type.Name.Equals("RepositoryBase`1"))                               
                               .ToList();
                services.AddScoped(typeInf, repositoryClass[0]);
            }
        }
    }
}
