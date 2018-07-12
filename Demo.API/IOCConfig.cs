using AutoMapper;
using Demo.BLL;
using Demo.BLL.IService;
using Demo.BLL.Service;
using Demo.DAL;
using Demo.DAL.Abstract;
using Demo.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API
{
    /// <summary>
    /// IOC configartion 
    /// </summary>
    public static class IOCConfig
    {
        #region Register Service

        /// <summary>
        /// Register IOC configartion 
        /// </summary>
        public static void Register(IServiceCollection services)
        {
            // Add Automapper
            services.AddAutoMapper();

            // Add Services
            services.AddScoped<IBookService, BookService>();

            // Add Repository
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAntiForgeryCookieService, AntiForgeryCookieService>();
            services.AddScoped<IUnitOfWork, DemoDbContext>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IDbInitializerService, DbInitializerService>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();
            services.AddScoped<ITokenValidatorService, TokenValidatorService>();
        }

        #endregion
    }
}
