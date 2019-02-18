using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.CrossCutting.Identity.Data;
using BeBlueApi.Infra.CrossCutting.Identity.Models;
using BeBlueApi.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeBlueApi.Infra.Data.Helpers
{
    public static class DbMigrationHelpers
    {
        /// <summary>
        /// Generate migrations before running this method, you can use command bellow:
        /// Nuget package manager: Add-Migration DbInit -context AdminDbContext -output Data/Migrations
        /// Dotnet CLI: dotnet ef migrations add DbInit -c AdminDbContext -o Data/Migrations
        /// </summary>
        /// <param name="host"></param>
        public static async Task EnsureSeedData(IWebHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                await EnsureSeedData(services);
            }
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                appContext.Database.Migrate();

                var eventContext = scope.ServiceProvider.GetRequiredService<EventStoreSQLContext>();
                eventContext.Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<BeblueDbContext>();
                context.Database.Migrate();
                await EnsureSeedContextData(context);

            }
        }
        public static async Task EnsureSeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetService<ISpotifyApiService>();

                if (!context.Products.Any())
                {
                    // Seed Here
                }

                context.SaveChanges();
            }

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                appContext.Database.Migrate();

                var eventContext = scope.ServiceProvider.GetRequiredService<EventStoreSQLContext>();
                eventContext.Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<BeblueDbContext>();
                context.Database.Migrate();
                await EnsureSeedContextData(context);

            }
        }
        /// <summary>
        /// Generate default clients, identity and api resources
        /// </summary>
        private static async Task EnsureSeedContextData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Create admin user
            if (await userManager.FindByNameAsync(Users.AdminUserName) != null) return;

            var user = new ApplicationUser
            {
                UserName = Users.AdminUserName,
                Email = Users.AdminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, Users.AdminPassword);
        }

        /// <summary>
        /// Generate default clients, identity and api resources
        /// </summary>
        private static async Task EnsureSeedContextData(BeblueDbContext context)
        {
            if (!context.MusicGender.Any() && !context.Cashback.Any())
            {
                foreach (var musicGender in EnsureSeedMusicGenderData())
                {
                    await context.MusicGender.AddAsync(musicGender);

                    for (int i = 0; i < 7; i++)
                    {
                        var DayOfWeek = (DayOfWeek)i;
                        decimal percent = 0;

                        #region Calculate percent of cashback
                        switch (DayOfWeek)
                        {
                            case DayOfWeek.Friday:
                                if (musicGender.Description == "POP")
                                    percent = 0.15M;
                                else if (musicGender.Description == "MPB")
                                    percent = 0.25M;
                                else if (musicGender.Description == "CLASSIC")
                                    percent = 0.18M;
                                else if (musicGender.Description == "ROCK")
                                    percent = 0.2M;
                                break;
                            case DayOfWeek.Monday:
                                if (musicGender.Description == "POP")
                                    percent = 0.07M;
                                else if (musicGender.Description == "MPB")
                                    percent = 0.05M;
                                else if (musicGender.Description == "CLASSIC")
                                    percent = 0.03M;
                                else if (musicGender.Description == "ROCK")
                                    percent = 0.1M;
                                break;
                            case DayOfWeek.Saturday:
                                if (musicGender.Description == "POP")
                                    percent = 0.2M;
                                else if (musicGender.Description == "MPB")
                                    percent = 0.3M;
                                else if (musicGender.Description == "CLASSIC")
                                    percent = 0.25M;
                                else if (musicGender.Description == "ROCK")
                                    percent = 0.4M;
                                break;
                            case DayOfWeek.Sunday:
                                if (musicGender.Description == "POP")
                                    percent = 0.25M;
                                else if (musicGender.Description == "MPB")
                                    percent = 0.3M;
                                else if (musicGender.Description == "CLASSIC")
                                    percent = 0.35M;
                                else if (musicGender.Description == "ROCK")
                                    percent = 0.4M;
                                break;
                            case DayOfWeek.Thursday:
                                if (musicGender.Description == "POP")
                                    percent = 0.1M;
                                else if (musicGender.Description == "MPB")
                                    percent = 0.2M;
                                else if (musicGender.Description == "CLASSIC")
                                    percent = 0.13M;
                                else if (musicGender.Description == "ROCK")
                                    percent = 0.15M;
                                break;
                            case DayOfWeek.Tuesday:
                                if (musicGender.Description == "POP")
                                    percent = 0.06M;
                                else if (musicGender.Description == "MPB")
                                    percent = 0.1M;
                                else if (musicGender.Description == "CLASSIC")
                                    percent = 0.05M;
                                else if (musicGender.Description == "ROCK")
                                    percent = 0.15M;
                                break;
                            case DayOfWeek.Wednesday:
                                if (musicGender.Description == "POP")
                                    percent = 0.02M;
                                else if (musicGender.Description == "MPB")
                                    percent = 0.15M;
                                else if (musicGender.Description == "CLASSIC")
                                    percent = 0.08M;
                                else if (musicGender.Description == "ROCK")
                                    percent = 0.15M;
                                break;
                        }

                        #endregion
                        await context.Cashback.AddAsync(new Cashback(new Guid(), musicGender.Id, DayOfWeek, percent));
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        await context.DiscMusic.AddAsync(new DiscMusic(new Guid(), i.ToString(), musicGender.Id, 10));
                    }
                }
            }

            await context.SaveChangesAsync();
        }

        private static ICollection<MusicGender> EnsureSeedMusicGenderData()
        {
            var records = new List<MusicGender>
            {
                new MusicGender(new Guid(),"CLASSIC"),
                new MusicGender(new Guid(), "MPB"),
                new MusicGender(new Guid(), "POP"),
                new MusicGender(new Guid(), "ROCK")
            };
            return records;
        }

    }
}
