using DataAccessLayer.Database;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,
            IConfiguration configuration, 
            string connectionString)
        {
            services.AddDbContext<AppDatabase>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDatabase>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAuthentication("Cookies")
                .AddCookie(options =>
            {
                options.LoginPath = "/Home/SignUp";
            });

            return services;
        }
    }
}
