using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services,
            IConfiguration configuration,
            string connectionString)
        {
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IPhoneService, PhoneService>();

            services.AddDataAccessLayer(configuration, connectionString);

            return services;
        }
    }
}
