﻿using BeBlueApi.Application.Interfaces;
using BeBlueApi.Infra.CrossCutting.Identity.Autorization;
using BeBlueApi.Infra.CrossCutting.Identity.Data;
using BeBlueApi.Infra.CrossCutting.Identity.Models;
using BeBlueApi.Infra.CrossCutting.IoC;
using BeBlueApi.Infra.Data.Helpers;
using BeBlueApi.WebApi.Configurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace BeBlueApi.WebApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddWebApi(options =>
            //{
            //    options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
            //    options.UseCentralRoutePrefix(new RouteAttribute("api/v1"));
            //});
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v1"));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapperSetup();

            services.AddHttpClient("Spotify_API", client=>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string baseURI = Configuration.GetSection("SpotifyApi:Endpoint").Value;
                string clientId = Configuration.GetSection("SpotifyApi:ClientId").Value;
                string clientSecret = Configuration.GetSection("SpotifyApi:ClientSecret").Value;
                client.BaseAddress = new Uri(String.Format(baseURI,clientId));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWriteSalesData", policy => policy.Requirements.Add(new ClaimRequirement("Sales", "Write")));
                options.AddPolicy("CanRemoveSalesData", policy => policy.Requirements.Add(new ClaimRequirement("Sales", "Remove")));
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "BeBlue Project",
                    Description = "BeBlueAPI Swagger surface"
                });
            });

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);

            //Console.WriteLine("******* SEED SENDO EXECUTADO!!! ****************");
            DbMigrationHelpers.EnsureSeedData(services.BuildServiceProvider()).GetAwaiter().GetResult();
            //Console.WriteLine("******* SEED FINALIZADO !!! ****************");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Beblue API v1.1");
            });

            var spotifyApi = app.ApplicationServices.GetService<ISpotifyApiService>();
            spotifyApi.ConnectSpotifyApi();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
