using BeblueApi.Domain.Core.Commands;
using System;

namespace BeBlueApi.Domain.Commands
{
    public abstract class DiscMusicCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public Guid IdGender { get; protected set; }

        public decimal Price { get; protected set; }
    }
}
