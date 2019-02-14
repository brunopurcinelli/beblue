using BeBlueApi.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BeBlueApi.Domain.EventsHandlers
{
    public class SalesEventHandler :
        INotificationHandler<SalesRegisteredEvent>,
        INotificationHandler<SalesUpdatedEvent>,
        INotificationHandler<SalesRemovedEvent>
    {
        public Task Handle(SalesUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(SalesRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(SalesRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
