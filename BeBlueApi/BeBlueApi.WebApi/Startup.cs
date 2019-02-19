using BeBlueApi.Application.Interfaces;
using BeBlueApi.Infra.CrossCutting.Identity.Autorization;
using BeBlueApi.Infra.CrossCutting.Identity.Data;
using BeBlueApi.Infra.CrossCutting.Identity.Models;
using BeBlueApi.Infra.CrossCutting.IoC;
using BeBlueApi.Infra.Data.Helpers;
using BeBlueApi.WebApi.Configurations;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Extensions;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler;
using FluentSpotifyApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private const string SpotifyAuthenticationScheme = SpotifyDefaults.AuthenticationScheme;

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

            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .ConfigurePipeline(pipeline=>pipeline
                        .AddAspNetCoreAuthorizationCodeFlow(
                            spotifyAuthenticationScheme: SpotifyAuthenticationScheme)));
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(
                        o =>
                        {
                            o.LoginPath = new PathString("/login");
                            o.LogoutPath = new PathString("/logout");
                        })
                    .AddSpotify(
                        SpotifyAuthenticationScheme,
                        o =>
                        {
                            o.ClientId = Configuration.GetSection("SpotifyApi:ClientId").Value;
                            o.ClientSecret = Configuration.GetSection("SpotifyApi:ClientSecret").Value;
                            o.Scope.Add("playlist-read-private");
                            o.Scope.Add("playlist-read-collaborative");
                            o.SaveTokens = true;
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

            //Console.WriteLine("******* SEED EXECUTANDO !!! ****************");
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

            app.Map(
                "/login",
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            // Return a challenge to invoke the Spotify authentication scheme
                            await context.ChallengeAsync(SpotifyAuthenticationScheme, properties: new AuthenticationProperties() { RedirectUri = "/", IsPersistent = true });
                        });
                });

            // Listen for requests on the /logout path, and sign the user out
            app.Map(
                "/logout",
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            // Sign the user out of the authentication middleware (i.e. it will clear the Auth cookie)
                            await context.SignOutAsync();

                            // Redirect the user to the home page after signing out
                            context.Response.Redirect("/");
                        });
                });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
