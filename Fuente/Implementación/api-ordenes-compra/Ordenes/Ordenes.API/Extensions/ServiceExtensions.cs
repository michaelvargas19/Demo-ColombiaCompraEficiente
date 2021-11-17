using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ordenes.Dominio.IServices.Command;
using Ordenes.Dominio.IServices.Queries;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Services.Command;
using Ordenes.Dominio.Services.Queries;
using Ordenes.Dominio.UnitOfWorks;
using Ordenes.Dominio.Util;
using Ordenes.Infraestructura.IRepositories.Command;
using Ordenes.Infraestructura.IRepositories.Query;
using Ordenes.Infraestructura.Repositories;
using Ordenes.Infraestructura.Repositories.Command;
using Ordenes.Infraestructura.SettingsDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ordenes.API.Extensions
{
    public static class ServicioExtensiones
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }



        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connection = config["ConnectionStrings:DB_EProcurement"];

            services.AddDbContext<ContextoDB>(options =>
                options.UseSqlServer(connection)
            );

        }




        public static void Configureinterfaces(this IServiceCollection services)
        {


            //Services - Query
            services.AddScoped<IOrdenesServiceQuery, OrdenesServiceQuery>();
            services.AddScoped<IOrdenesServiceCommand, OrdenesServiceCommand>();

            //Unit of work
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


            //Repository
            services.AddScoped(typeof(IRepositoryBaseQuery<>), typeof(RepositoryBaseQuery<>));
            services.AddScoped(typeof(IRepositoryBaseCommand<>), typeof(RepositoryBaseCommand<>));


            //Utils
            services.AddScoped<IUtils, Utils>();


        }



        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Órdenes de Compra API",
                    Description = "API de Catálogos",
                    //TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Name = "TEST",
                        Email = "michavarg9@gmail.com",
                        Url = new Uri("https://puj.org.co/"),
                    }

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Inserte el JWT con Bearer en el campo de texto",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }
    }
}
