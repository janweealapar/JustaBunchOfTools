using JBOT.Application.Common.Interfaces;
using JBOT.Infrastructure.Persistence;
using JBOT.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("JbotDatabase"),
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)), ServiceLifetime.Transient);

            //services.AddDbContext<ValidateDBContext>();
            //services.AddDbContext<ValidateDBContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("ValidateDatabase"),
            //    b => b.MigrationsAssembly(typeof(ValidateDBContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddScoped<IApplicationDBContext, ApplicationDBContext>();
            services.AddDbContext<IValidateDBContext, ValidateDBContext>();
            services.AddScoped<IValidationContextFactory, ValidationContextFactory>();

            return services;
        }

        public static IServiceCollection AddMapper<T>(this IServiceCollection services) where T: class
        {
            services.AddAutoMapper(typeof(T));
            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddAutoMapper(assemblies);
            return services;
        }
    }
}
