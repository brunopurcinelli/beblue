using BeBlueApi.Application.Interfaces;
using BeBlueApi.Application.Services;
using BeBlueApi.Domain.CommandHandlers;
using BeBlueApi.Domain.Commands;
using BeblueApi.Domain.Core.Bus;
using BeblueApi.Domain.Core.Events;
using BeblueApi.Domain.Core.Notifications;
using BeBlueApi.Domain.Events;
using BeBlueApi.Domain.EventsHandlers;
using BeBlueApi.Domain.Interfaces;
using BeblueApi.Infra.CrossCutting.Bus;
using BeBlueApi.Infra.CrossCutting.Identity.Autorization;
using BeBlueApi.Infra.CrossCutting.Identity.Models;
using BeBlueApi.Infra.CrossCutting.Identity.Services;
using BeBlueApi.Infra.Data.Context;
using BeBlueApi.Infra.Data.EventSourcing;
using BeBlueApi.Infra.Data.Repository;
using BeBlueApi.Infra.Data.Repository.EventSourcing;
using BeBlueApi.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BeBlueApi.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ISpotifyApiService, SpotifyApiService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            
            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<IDiscMusicAppService, DiscMusicAppService>();
            services.AddScoped<ISalesAppService, SalesAppService>();
            services.AddScoped<ISalesLineAppService, SalesLineAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CashbackRegisteredEvent>, CashbackEventHandler>();
            services.AddScoped<INotificationHandler<CashbackUpdatedEvent>, CashbackEventHandler>();
            services.AddScoped<INotificationHandler<CashbackRemovedEvent>, CashbackEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewSalesCommand, bool>, SalesCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateSalesCommand, bool>, SalesCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveSalesCommand, bool>, SalesCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterNewSalesLineCommand, bool>, SalesLineCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateSalesLineCommand, bool>, SalesLineCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveSalesLineCommand, bool>, SalesLineCommandHandler>();

            // Infra - Data
            services.AddScoped<ICashbackRepository, CashbackRepository>();
            services.AddScoped<IMusicGenderRepository, MusicGenderRepository>();
            services.AddScoped<IDiscMusicRepository, DiscMusicRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<ISalesLineRepository, SalesLineRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<BeblueDbContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();

            // Spotify API
            //services.AddTransient<ISpotifyApiService, SpotifyApiService>();
        }
    }
}
