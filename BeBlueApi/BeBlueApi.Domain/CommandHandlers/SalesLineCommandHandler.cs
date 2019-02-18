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
        IRequestHandler<RegisterNewSalesLineCommand, bool>,
        IRequestHandler<UpdateSalesLineCommand, bool>,
        IRequestHandler<RemoveSalesLineCommand, bool>
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

        public Task<bool> Handle(RegisterNewSalesLineCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var SalesLine = new SalesLine(Guid.NewGuid(), message.IdSales, message.IdDisc, "", message.Quantity, message.PriceUnit, message.SalesPrice, message.Cashback);

            if (_SalesRepository.GetById(SalesLine.IdSales) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Ordem de Venda não existe"));
                return Task.FromResult(false);
            }

            if (_DiscMusicRepository.GetById(SalesLine.IdItem) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "O disco não existe"));
                return Task.FromResult(false);
            }

            _SalesLineRepository.Add(SalesLine);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesLineRegisteredEvent(SalesLine.Id, SalesLine.IdSales, SalesLine.IdItem, SalesLine.Quantity, SalesLine.PriceUnit,SalesLine.SalesPrice, SalesLine.Cashback));
            }

            return Task.FromResult(false);
        }

        public Task<bool> Handle(UpdateSalesLineCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var SalesLine = new SalesLine(Guid.NewGuid(), message.IdSales, message.IdDisc, "", message.Quantity, message.PriceUnit, message.SalesPrice, message.Cashback);
            var existingSalesLine = _SalesLineRepository.GetById(SalesLine.Id);

            if (existingSalesLine != null && existingSalesLine.Id != SalesLine.Id)
            {
                if (!existingSalesLine.Equals(SalesLine))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "Ocorreu um erro ao salvar o item de venda"));
                    return Task.FromResult(false);
                }
            }

            _SalesLineRepository.Update(SalesLine);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesLineUpdatedEvent(SalesLine.Id, SalesLine.IdSales, SalesLine.IdItem, SalesLine.DiscName, SalesLine.Quantity, SalesLine.PriceUnit, SalesLine.Cashback));
            }

            return Task.FromResult(false);
        }

        public Task<bool> Handle(RemoveSalesLineCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _SalesLineRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesLineRemovedEvent(message.Id));
            }

            return Task.FromResult(false);
        }

        public void Dispose()
        {
            _SalesLineRepository.Dispose();
        }

    }
}
