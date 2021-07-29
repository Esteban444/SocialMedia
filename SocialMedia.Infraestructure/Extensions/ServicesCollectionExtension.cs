using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.CustonEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Infraestructure.Interfaces;
using SocialMedia.Infraestructure.Options;
using SocialMedia.Infraestructure.Repositories;
using SocialMedia.Infraestructure.Services;
using System;
using System.IO;
using System.Reflection;

namespace SocialMedia.Infraestructure.Extensions
{
    public  static class ServicesCollectionExtension
    {
        // agregado para refactorizar el starup
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration Configuration) 
        {
            services.AddDbContext<RedContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration Configuration) 
        {
            services.Configure<PaginationOptions>(options => Configuration.GetSection("Pagination").Bind(options)); // agregado en paginacion para valores por defeto de appsettings.json

            services.Configure<PasswordOptions>(options => Configuration.GetSection("PasswordOptions").Bind(options)); //agregado por lo de la encriptacion clase carpeta options infraestutura

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            // Dependencias
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IPasswordService, PasswordService>();
            //services.AddTransient<IPostRepository, PostRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>)); // para utilizar el repositorio Base Repository

            services.AddTransient<IUnitOfWork, UnitOfWork>(); // para utilizar la dependencia de UniOfWork

            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });// para la injeccion de la IUriService
            return services;
        }

        
    }
}
