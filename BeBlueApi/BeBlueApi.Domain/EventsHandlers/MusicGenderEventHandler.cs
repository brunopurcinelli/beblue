using BeBlueApi.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BeBlueApi.Domain.EventsHandlers
{
    public class MusicGenderEventHandler :
        INotificationHandler<MusicGenderRegisteredEvent>,
        INotificationHandler<MusicGenderUpdatedEvent>,
        INotificationHandler<MusicGenderRemovedEvent>
    {
        public Task Handle(MusicGenderUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(MusicGenderRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(MusicGenderRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
