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
    public class CashbackCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCashbackCommand>,
        IRequestHandler<UpdateCashbackCommand>,
        IRequestHandler<RemoveCashbackCommand>
    {
        private readonly ICashbackRepository _CashbackRepository;
        private readonly IMediatorHandler Bus;

        public CashbackCommandHandler(ICashbackRepository CashbackRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      MediatR.INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _CashbackRepository = CashbackRepository;
            Bus = bus;
        }

        public Task<Unit> Handle(RegisterNewCashbackCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var Cashback = new Cashback(Guid.NewGuid(), message.MusicGender, message.WeekDay, message.Percent);

            if (_CashbackRepository.GetByWeekDay(Cashback.WeekDay) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Cashback e-mail has already been taken."));
                return (Task<Unit>)Task.CompletedTask;
            }

            _CashbackRepository.Add(Cashback);

            if (Commit())
            {
                Bus.RaiseEvent(new CashbackRegisteredEvent(Cashback.Id, Cashback.MusicGender, Cashback.WeekDay, Cashback.Percent));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(UpdateCashbackCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var Cashback = new Cashback(message.Id, message.MusicGender, message.WeekDay, message.Percent);
            var existingCashback = _CashbackRepository.GetById(Cashback.Id);

            if (existingCashback != null && existingCashback.Id != Cashback.Id)
            {
                if (!existingCashback.Equals(Cashback))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Cashback e-mail has already been taken."));
                    return (Task<Unit>)Task.CompletedTask;
                }
            }

            _CashbackRepository.Update(Cashback);

            if (Commit())
            {
                Bus.RaiseEvent(new CashbackUpdatedEvent(Cashback.Id, Cashback.MusicGender, Cashback.WeekDay, Cashback.Percent));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(RemoveCashbackCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            _CashbackRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new CashbackRemovedEvent(message.Id));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public void Dispose()
        {
            _CashbackRepository.Dispose();
        }
        
    }
}
