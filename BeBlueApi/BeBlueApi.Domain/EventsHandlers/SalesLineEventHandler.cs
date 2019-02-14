using BeBlueApi.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BeBlueApi.Domain.EventsHandlers
{
    public class SalesLineEventHandler :
        INotificationHandler<SalesLineRegisteredEvent>,
        INotificationHandler<SalesLineUpdatedEvent>,
        INotificationHandler<SalesLineRemovedEvent>
    {
        public Task Handle(SalesLineUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(SalesLineRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(SalesLineRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
