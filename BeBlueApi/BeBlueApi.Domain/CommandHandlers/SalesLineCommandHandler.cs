using System;
using System.Threading;
using System.Threading.Tasks;
using BeblueApi.Domain.Core.Bus;
using BeblueApi.Domain.Core.Notifications;
using BeBlueApi.Domain.Commands;
using BeBlueApi.Domain.Events;
using BeBlueApi.Domain.Interfaces;
using BeBlueApi.Domain.Models;
using MediatR;

namespace BeBlueApi.Domain.CommandHandlers
{
    public class SalesLineCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewSalesLineCommand>,
        IRequestHandler<UpdateSalesLineCommand>,
        IRequestHandler<RemoveSalesLineCommand>
    {
        private readonly ISalesRepository _SalesRepository;
        private readonly ISalesLineRepository _SalesLineRepository;
        private readonly IDiscMusicRepository _DiscMusicRepository;
        private readonly IMediatorHandler Bus;

        public SalesLineCommandHandler(ISalesLineRepository salesLineRepository,
                                       ISalesRepository salesRepository,
                                       IDiscMusicRepository discMusicRepository,
                                       IUnitOfWork uow,
                                       IMediatorHandler bus,
                                       INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _SalesRepository = salesRepository;
            _SalesLineRepository = salesLineRepository;
            _DiscMusicRepository = discMusicRepository;
            Bus = bus;
        }

        public Task<Unit> Handle(RegisterNewSalesLineCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var SalesLine = new SalesLine(Guid.NewGuid(), message.IdSales,message.IdItem, message.DiscName, message.Quantity, message.PriceUnit, message.Cashback);

            if (_SalesRepository.GetById(SalesLine.IdSales) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Ordem de Venda não existe"));
                return (Task<Unit>)Task.CompletedTask;
            }

            if (_DiscMusicRepository.GetById(SalesLine.IdItem) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "O disco não existe"));
                return (Task<Unit>)Task.CompletedTask;
            }

            _SalesLineRepository.Add(SalesLine);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesLineRegisteredEvent(SalesLine.Id, SalesLine.IdSales, SalesLine.IdItem, SalesLine.DiscName, SalesLine.Quantity, SalesLine.PriceUnit, SalesLine.Cashback));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(UpdateSalesLineCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var SalesLine = new SalesLine(Guid.NewGuid(), message.IdSales, message.IdItem, message.DiscName, message.Quantity, message.PriceUnit, message.Cashback);
            var existingSalesLine = _SalesLineRepository.GetById(SalesLine.Id);

            if (existingSalesLine != null && existingSalesLine.Id != SalesLine.Id)
            {
                if (!existingSalesLine.Equals(SalesLine))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "Ocorreu um erro ao salvar o item de venda"));
                    return (Task<Unit>)Task.CompletedTask;
                }
            }

            _SalesLineRepository.Update(SalesLine);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesLineUpdatedEvent(SalesLine.Id, SalesLine.IdSales, SalesLine.IdItem, SalesLine.DiscName, SalesLine.Quantity, SalesLine.PriceUnit, SalesLine.Cashback));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(RemoveSalesLineCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            _SalesLineRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesLineRemovedEvent(message.Id));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public void Dispose()
        {
            _SalesLineRepository.Dispose();
        }

    }
}
