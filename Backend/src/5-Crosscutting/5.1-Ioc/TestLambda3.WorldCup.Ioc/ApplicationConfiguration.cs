using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLambda3.WorldCup.Application.App;
using TestLambda3.WorldCup.Application.Interface;
using TestLambda3.WorldCup.Application.Interface.Service;
using TestLambda3.WorldCup.Application.Shared;
using TestLambda3.WorldCup.Domain.Interfaces.Repository;
using TestLambda3.WorldCup.Infrastructure;

namespace TestLambda3.WorldCup.Ioc
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IBaseNotification, BaseNotification>();
            services.AddScoped<IWorldCupRepository, WorldCupRepository>();
            services.AddScoped<IWorldCupApp, WorldCupApp>();
            return services;
        }
    }
}
