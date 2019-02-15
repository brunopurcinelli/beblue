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
    public class SalesCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewSalesCommand>,
        IRequestHandler<UpdateSalesCommand>,
        IRequestHandler<RemoveSalesCommand>
    {
        private readonly ISalesRepository _SalesRepository;
        private readonly IMediatorHandler Bus;

        public SalesCommandHandler(ISalesRepository SalesRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _SalesRepository = SalesRepository;
            Bus = bus;
        }

        public Task<Unit> Handle(RegisterNewSalesCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var Sales = new Sales(Guid.NewGuid(), message.SalesDate, message.TotalAmount, message.TotalCashback);
            
            _SalesRepository.Add(Sales);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesRegisteredEvent(Sales.Id, Sales.SalesDate, Sales.TotalAmount, Sales.TotalCashback));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(UpdateSalesCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var Sales = new Sales(message.Id, message.SalesDate, message.TotalAmount, message.TotalCashback);
            var existingSales = _SalesRepository.GetById(Sales.Id);

            if (existingSales != null && existingSales.Id != Sales.Id)
            {
                if (!existingSales.Equals(Sales))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Sales e-mail has already been taken."));
                    return (Task<Unit>)Task.CompletedTask;
                }
            }

            _SalesRepository.Update(Sales);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesUpdatedEvent(Sales.Id, Sales.SalesDate, Sales.TotalAmount, Sales.TotalCashback));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(RemoveSalesCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            _SalesRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesRemovedEvent(message.Id));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public void Dispose()
        {
            _SalesRepository.Dispose();
        }

    }
}
