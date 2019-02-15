using BeblueApi.Domain.Core.Commands;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Domain.Commands
{
    public abstract class SalesCommand : Command
    {
        public Guid Id { get; protected set; }

        public DateTime SalesDate { get; protected set; }

        public decimal TotalAmount { get; protected set; }

        public decimal TotalCashback { get; protected set; }
    }
}
