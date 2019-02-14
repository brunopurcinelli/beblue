using BeblueApi.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Domain.Commands
{
    public abstract class CashbackCommand : Command
    {
        public Guid Id { get; protected set; }

        public Guid IdGender { get; protected set; }

        public string WeekDay { get; protected set; }

        public decimal Percent { get; protected set; }
    }
}
