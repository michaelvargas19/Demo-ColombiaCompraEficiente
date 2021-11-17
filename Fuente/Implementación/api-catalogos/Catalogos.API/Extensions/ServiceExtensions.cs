﻿using Catalogos.Dominio.IServices.Queries;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Services.Queries;
using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.IRepositories.Query;
using Catalogos.Infraestructura.Repositories;
using Catalogos.Infraestructura.SettingsDB;
using Catalogos.Infraestructura.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Catalogos.API.Extensions
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
            services.AddScoped<ICatalogosServiceQuery, CatalogosServiceQuery>();
            services.AddScoped<IProductosServiceQuery, ProductosServiceQuery>();

            //Unit of work
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


            //Repository
            services.AddScoped(typeof(IRepositoryBaseQuery<>), typeof(RepositoryBaseQuery<>));
            

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
                    Title = "Catálogos y Productos API",
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
