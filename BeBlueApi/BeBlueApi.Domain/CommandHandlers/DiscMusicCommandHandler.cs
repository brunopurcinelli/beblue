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
    public class DiscMusicCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewDiscMusicCommand>,
        IRequestHandler<UpdateDiscMusicCommand>,
        IRequestHandler<RemoveDiscMusicCommand>
    {
        private readonly IDiscMusicRepository _DiscMusicRepository;
        private readonly IMusicGenderRepository _MusicGenderRepository;
        private readonly IMediatorHandler Bus;

        public DiscMusicCommandHandler(IDiscMusicRepository discMusicRepository,
                                       IMusicGenderRepository musicGenderRepository, 
                                       IUnitOfWork uow,
                                       IMediatorHandler bus,
                                       INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _DiscMusicRepository = discMusicRepository;
            _MusicGenderRepository = musicGenderRepository;
            Bus = bus;
        }

        public Task<Unit> Handle(RegisterNewDiscMusicCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var DiscMusic = new DiscMusic(Guid.NewGuid(), message.Name, message.IdGender, message.Price);

            if (_MusicGenderRepository.GetById(DiscMusic.IdGender) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "O tipo de gênero não existe."));
                return (Task<Unit>)Task.CompletedTask;
            }

            _DiscMusicRepository.Add(DiscMusic);

            if (Commit())
            {
                Bus.RaiseEvent(new DiscMusicRegisteredEvent(DiscMusic.Id, DiscMusic.IdGender, DiscMusic.Name, DiscMusic.Price));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(UpdateDiscMusicCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            var DiscMusic = new DiscMusic(Guid.NewGuid(), message.Name, message.IdGender, message.Price);
            var existingDiscMusic = _DiscMusicRepository.GetById(DiscMusic.Id);

            if (existingDiscMusic != null && existingDiscMusic.Id != DiscMusic.Id)
            {
                if (!existingDiscMusic.Equals(DiscMusic))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "O Item está incorreto ou não existe"));
                    return (Task<Unit>)Task.CompletedTask;
                }
            }

            _DiscMusicRepository.Update(DiscMusic);

            if (Commit())
            {
                Bus.RaiseEvent(new DiscMusicRegisteredEvent(DiscMusic.Id, DiscMusic.IdGender, DiscMusic.Name, DiscMusic.Price));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public Task<Unit> Handle(RemoveDiscMusicCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return (Task<Unit>)Task.CompletedTask;
            }

            _DiscMusicRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new DiscMusicRemovedEvent(message.Id));
            }

            return (Task<Unit>)Task.CompletedTask;
        }

        public void Dispose()
        {
            _DiscMusicRepository.Dispose();
        }

    }
}
