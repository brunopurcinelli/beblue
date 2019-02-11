using BeblueApi.Domain.Core.Bus;
using BeblueApi.Domain.Core.Events;
using BeblueApi.Domain.Core.Notifications;
using BeblueApi.Infra.CrossCutting.Bus;
using BeBlueApi.Application.Interfaces;
using BeBlueApi.Application.Services;
using BeBlueApi.Domain.CommandHandlers;
using BeBlueApi.Domain.Commands;
using BeBlueApi.Domain.Events;
using BeBlueApi.Domain.EventsHandlers;
using BeBlueApi.Domain.Interfaces;
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

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<ICashbackAppService, CashbackAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CashbackRegisteredEvent>, CashbackEventHandler>();
            services.AddScoped<INotificationHandler<CashbackUpdatedEvent>, CashbackEventHandler>();
            services.AddScoped<INotificationHandler<CashbackRemovedEvent>, CashbackEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCashbackCommand>, CashbackCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCashbackCommand>, CashbackCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCashbackCommand>, CashbackCommandHandler>();

            // Infra - Data
            services.AddScoped<ICashbackRepository, CashbackRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<Data.Context.BeblueDbContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
