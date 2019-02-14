using BeblueApi.Domain.Core.Commands;
using System;

namespace BeBlueApi.Domain.Commands
{
    public abstract class SalesLineCommand : Command
    {
        public Guid Id { get; set; }

        public Guid IdSales { get; set; }

        public Guid IdItem { get; set; }

        public string DiscName { get; set; }

        public int Quantity { get; set; }

        public decimal PriceUnit { get; set; }

        public decimal SalesPrice { get; set; }

        public decimal Cashback { get; set; }
    }
}
