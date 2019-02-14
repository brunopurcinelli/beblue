using BeblueApi.Domain.Core.Commands;
using System;

namespace BeBlueApi.Domain.Commands
{
    public abstract class MusicGenderCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Description { get; protected set; }
    }
}
