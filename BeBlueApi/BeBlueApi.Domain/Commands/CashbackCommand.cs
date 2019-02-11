using BeblueApi.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Domain.Commands
{
    public abstract class CashbackCommand : Command
    {
        public Guid Id { get; protected set; }

        public string MusicGender { get; protected set; }

        public string WeekDay { get; protected set; }

        public double Percent { get; protected set; }
    }
}
