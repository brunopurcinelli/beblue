using System;
using System.Collections.Generic;
using System.Linq;
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
        IRequestHandler<RegisterNewSalesCommand, bool>,
        IRequestHandler<UpdateSalesCommand, bool>,
        IRequestHandler<RemoveSalesCommand, bool>
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IDiscMusicRepository _discMusicRepository;
        private readonly ICashbackRepository _cashbackRepository;
        private readonly IMediatorHandler Bus;

        public SalesCommandHandler(ISalesRepository salesRepository,
                                    IDiscMusicRepository discMusicRepository,
                                    ICashbackRepository cashbackRepository,
                                    IUnitOfWork uow,
                                    IMediatorHandler bus,
                                    INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _salesRepository = salesRepository;
            _discMusicRepository = discMusicRepository;
            _cashbackRepository = cashbackRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewSalesCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var Sales = new Sales(Guid.NewGuid(), message.SalesDate, message.TotalAmount, message.TotalCashback);

            decimal totalAmount = 0;
            decimal totalCashback = 0;

            Sales.Lines = new List<SalesLine>();

            foreach (var item in message.SalesLines)
            {
                var disc = _discMusicRepository.GetById(item.IdDisc);
                if (disc == null)
                {
                    NotifyValidationErrors(message);
                    return Task.FromResult(false);
                }
                var cashback = _cashbackRepository.GetByWeekDay(disc.IdGender, Sales.SalesDate);

                var salesPrice = item.Quantity * disc.Price;
                var cashbackAmount = salesPrice * cashback.Percent;

                totalAmount += salesPrice;
                totalCashback += cashbackAmount;

                Sales.Lines.Add(new SalesLine(Guid.NewGuid(), Sales.Id, item.IdDisc, disc.Name, item.Quantity, item.PriceUnit, salesPrice, cashbackAmount));
            }

            Sales.TotalAmount = totalAmount;
            Sales.TotalCashback = totalCashback;

            _salesRepository.Add(Sales);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesRegisteredEvent(
                    Sales.Id, 
                    Sales.SalesDate, 
                    Sales.TotalAmount, 
                    Sales.TotalCashback, 
                    Sales.Lines.ToList().ConvertAll(x => 
                                                    new SalesLineRegisteredEvent(
                                                        x.Id,
                                                        x.IdSales,
                                                        x.IdItem,
                                                        x.Quantity,
                                                        x.PriceUnit,
                                                        x.SalesPrice,
                                                        x.Cashback))));
            }

            return Task.FromResult(false);
        }

        public Task<bool> Handle(UpdateSalesCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var Sales = new Sales(message.Id, message.SalesDate, message.TotalAmount, message.TotalCashback);
            var existingSales = _salesRepository.GetById(Sales.Id);

            if (existingSales != null && existingSales.Id != Sales.Id)
            {
                if (!existingSales.Equals(Sales))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Sales e-mail has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _salesRepository.Update(Sales);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesUpdatedEvent(Sales.Id, Sales.SalesDate, Sales.TotalAmount, Sales.TotalCashback));
            }

            return Task.FromResult(false);
        }

        public Task<bool> Handle(RemoveSalesCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _salesRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new SalesRemovedEvent(message.Id));
            }

            return Task.FromResult(false);
        }

        public void Dispose()
        {
            _salesRepository.Dispose();
        }

    }
}
