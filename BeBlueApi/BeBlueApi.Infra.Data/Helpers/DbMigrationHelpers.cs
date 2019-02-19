using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.CrossCutting.Identity.Data;
using BeBlueApi.Infra.CrossCutting.Identity.Models;
using BeBlueApi.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            foreach (var musicGender in EnsureSeedMusicGenderData())
            {
                if (!context.MusicGender.Any())
                    await context.MusicGender.AddAsync(musicGender);

                if (!context.Cashback.Any())
                {
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
                }

                if (!context.DiscMusic.Any())
                {
                    var listDisc = new List<DiscMusic>();
                    switch (musicGender.Description)
                    {
                        case "CLASSIC":
                            listDisc = await LoadAlbunsBySpotify(musicGender, "classical");
                            listDisc.ForEach(e => context.DiscMusic.AddAsync(e));
                            break;
                        case "MPB":
                            listDisc = await LoadAlbunsBySpotify(musicGender, "brazilian");
                            listDisc.ForEach(e => context.DiscMusic.AddAsync(e));
                            break;
                        case "POP":
                            listDisc = await LoadAlbunsBySpotify(musicGender, "pop");
                            listDisc.ForEach(e => context.DiscMusic.AddAsync(e));
                            break;
                        case "ROCK":
                            listDisc = await LoadAlbunsBySpotify(musicGender, "rock");
                            listDisc.ForEach(e => context.DiscMusic.AddAsync(e));
                            break;
                    }
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task<List<DiscMusic>> LoadAlbunsBySpotify(MusicGender musicGender, string gender)
        {
            var listDisc = new List<DiscMusic>();
            var _spotify = await ConnectSpotifyApi();

            var list = await _spotify.GetCategoryPlaylistsAsync(gender, "BR");
            //var listCategories = await _spotify.GetCategoriesAsync("BR", "", 50);

            foreach (var item in list.Playlists.Items)
            {
                FullPlaylist playlist = _spotify.GetPlaylist("", item.Id);
                var listTracks = playlist.Tracks.Items;

                foreach (var track in listTracks)
                {
                    Random random = new Random();
                    var price = random.NextDouble() * (150.89 - 10.49) + 10.49;

                    listDisc.Add(new DiscMusic(new Guid(),
                                                track.Track.Album.Name,
                                                musicGender.Id,
                                                track.Track.Album.Id,
                                                track.Track.Artists.FirstOrDefault().Name,
                                                track.Track.Album.AlbumGroup,
                                                Convert.ToDecimal(price)));
                    if (listDisc.Count > 49)
                    {
                        return listDisc;
                    }
                }
            }

            return listDisc;
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

        private static async Task<SpotifyWebAPI> ConnectSpotifyApi()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            CredentialsAuth auth = new CredentialsAuth(config.GetSection("SpotifyApi:ClientId").Value, config.GetSection("SpotifyApi:ClientSecret").Value);
            Token token = await auth.GetToken();
            var _spotify = new SpotifyWebAPI() { TokenType = token.TokenType, AccessToken = token.AccessToken };

            //var listCategories = _spotify.GetCategories();
            //var list = _spotify.GetAlbumTracks()

            //foreach (var item in listCategories.Categories.Items)
            //{
            //    Console.WriteLine(item.Name);
            //}

            return _spotify;
        }
        private static async Task<SpotifyWebAPI> AuthSpotifyApi()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            AuthorizationCodeAuth auth =
                new AuthorizationCodeAuth(
                        config.GetSection("SpotifyApi:ClientId").Value,
                        config.GetSection("SpotifyApi:ClientSecret").Value,
                        "http://localhost:6410",
                        "http://localhost:6410",
                        SpotifyAPI.Web.Enums.Scope.PlaylistReadCollaborative);

            auth.AuthReceived += async (sender, payload) =>
            {
                auth.Stop();
                Token token = await auth.ExchangeCode(payload.Code);
                _spotifyWeb = new SpotifyWebAPI() { TokenType = token.TokenType, AccessToken = token.AccessToken };
                // Do requests with API client
            };
            auth.Start(); // Starts an internal HTTP Server
            auth.OpenBrowser();

            return _spotifyWeb;
        }
        private static SpotifyWebAPI _spotifyWeb;
    }
}
