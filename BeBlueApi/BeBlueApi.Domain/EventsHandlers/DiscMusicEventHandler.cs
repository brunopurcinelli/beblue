using BeBlueApi.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BeBlueApi.Domain.EventsHandlers
{
    public class DiscMusicEventHandler :
        INotificationHandler<DiscMusicRegisteredEvent>,
        INotificationHandler<DiscMusicUpdatedEvent>,
        INotificationHandler<DiscMusicRemovedEvent>
    {
        public Task Handle(DiscMusicUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(DiscMusicRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(DiscMusicRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
