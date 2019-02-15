using BeblueApi.Domain.Core.Commands;
using System;

namespace BeBlueApi.Domain.Commands
{
    public abstract class SalesLineCommand : Command
    {
        public Guid Id { get; protected set; }

        public Guid IdSales { get; protected set; }

        public Guid IdDisc { get; protected set; }

        public int Quantity { get; protected set; }

        public decimal PriceUnit { get; protected set; }

        public decimal SalesPrice { get; protected set; }

        public decimal Cashback { get; protected set; }
    }
}
