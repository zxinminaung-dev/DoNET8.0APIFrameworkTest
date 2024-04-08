using ChatApp.Web.DataAccess;
using ChatApp.Web.Model.Common;
using System.Reflection;

namespace ChatApp.Web.Infrastructure
{
    public static class ServiceExtensionCollection
    {
        public static void AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            services.AddScoped<IDbContext, DatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //filter the entity

            //var entityTypes = assembly.GetTypes().Where(x=>x.IsClass && !x.IsInterface && x.IsSubclassOf(typeof(BaseEntity)));
            //foreach(var entityType in entityTypes)
            //{
            //    var repositoryInterface = typeof(IRepository<>).MakeGenericType(entityType);
            //    var repositoryImplementation = typeof(RepositoryBase<>).MakeGenericType(entityType);

            //    // Register the repository services
                
            //    services.AddScoped(repositoryInterface, repositoryImplementation);
            //}

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
    }
}
