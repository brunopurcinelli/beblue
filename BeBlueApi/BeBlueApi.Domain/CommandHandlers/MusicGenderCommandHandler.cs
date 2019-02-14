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
    public class MusicGenderCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewMusicGenderCommand>,
        IRequestHandler<UpdateMusicGenderCommand>,
        IRequestHandler<RemoveMusicGenderCommand>
    {
        private readonly IMusicGenderRepository _MusicGenderRepository;
        private readonly IMediatorHandler Bus;

        public MusicGenderCommandHandler(IMusicGenderRepository MusicGenderRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      MediatR.INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _MusicGenderRepository = MusicGenderRepository;
            Bus = bus;
        }

        public Task<Unit> Handle(RegisterNewMusicGenderCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var MusicGender = new MusicGender(Guid.NewGuid(), message.Description);
            
            _MusicGenderRepository.Add(MusicGender);

            if (Commit())
            {
                Bus.RaiseEvent(new MusicGenderRegisteredEvent(MusicGender.Id, MusicGender.Description));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(UpdateMusicGenderCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var MusicGender = new MusicGender(message.Id, message.Description);
            var existingMusicGender = _MusicGenderRepository.GetById(MusicGender.Id);

            if (existingMusicGender != null && existingMusicGender.Id != MusicGender.Id)
            {
                if (!existingMusicGender.Equals(MusicGender))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The MusicGender e-mail has already been taken."));
                    return (Task<Unit>)Task.CompletedTask;
                }
            }

            _MusicGenderRepository.Update(MusicGender);

            if (Commit())
            {
                Bus.RaiseEvent(new MusicGenderUpdatedEvent(MusicGender.Id, MusicGender.Description));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(RemoveMusicGenderCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            _MusicGenderRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new MusicGenderRemovedEvent(message.Id));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public void Dispose()
        {
            _MusicGenderRepository.Dispose();
        }

    }
}
