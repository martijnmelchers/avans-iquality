using System;
using System.Linq;
using IQuality.DomainServices.Services;
using IQuality.Infrastructure.Database;
using IQuality.Models.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IQuality.Api.Extensions
{
    public static class StartupExtensions
    {
         public static IServiceCollection AddDependencies(this IServiceCollection services, IHostEnvironment environment)
        {
            // This automatically registers all services/repositories/etc.. All you need to do is add [Injectable] above your class
            // It finds the classes with assembly so for each project you need to add a .Concat()... with ANY class out of that project.
            foreach (var type in typeof(AuthenticationService).Assembly.GetExportedTypes()
                .Concat(typeof(DatabaseExtensions).Assembly.GetExportedTypes()))


            {
                var attributes = type.GetCustomAttributes(typeof(Injectable), true);
                if (attributes.Length == 0) continue;

                var injectable = (Injectable)attributes[0];
                var face = injectable.Interface ?? type.GetInterfaces().FirstOrDefault();
                var implementation = !environment.IsProduction() ? injectable.TestingDummy ?? type : type;

                switch (injectable.Type)
                {
                    case InjectionType.Scoped:
                        services.AddScoped(face, implementation);
                        break;
                    case InjectionType.Singleton:
                        services.AddSingleton(face, implementation);
                        break;
                    case InjectionType.Transient:
                        services.AddTransient(face, implementation);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return services;
        }

    }
}