using BeBlueApi.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeBlueApi.Domain.EventsHandlers
{
    public class CashbackEventHandler :
        INotificationHandler<CashbackRegisteredEvent>,
        INotificationHandler<CashbackUpdatedEvent>,
        INotificationHandler<CashbackRemovedEvent>
    {
        public Task Handle(CashbackUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CashbackRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CashbackRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
