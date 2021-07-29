using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using SocialMedia.Infraestructure.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SocialMedia.Infraestructure.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace SocialMedia.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Cofiguracion de automapper

            services.AddControllers(options => //esta landa para agregar el manejo de excepciones con GlobalExceptionFilter
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; /*esta linea es para 
                * ignorar los json nulos agregada en paginacion con metadata para que no muestre los json nulos en los otros metodos*/

            })// del punto en adelante es para desabilitar la validacion del modelo del apicontroller
            .ConfigureApiBehaviorOptions(options => {
                //options.SuppressModelStateInvalidFilter = true;
            });

            services.AddOptions(Configuration);

            services.AddDbContexts(Configuration);

            services.AddServices();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media Api", Version = "v1" });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                { 
                    Description = "Bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                         {
                            {
                              new OpenApiSecurityScheme
                              {
                                Reference = new OpenApiReference
                                      {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                      },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                            }
                        });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // agregado para la documentacion
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
            
            //registrar la autentificacion debe ser antes AddMVC
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Secretkey"]))
                };
            });

            //acontinuacion se registra un filtro de validacion de forma global
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            })//despues del punto es para agregar las validaciones de Validators
             .AddFluentValidation(options =>
             {
                  options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Social Media Api V1");
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
             

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
