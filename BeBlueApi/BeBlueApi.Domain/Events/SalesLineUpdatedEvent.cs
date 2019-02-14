using BeblueApi.Domain.Core.Events;
using System;

namespace BeBlueApi.Domain.Events
{
    public class SalesLineUpdatedEvent : Event
    {
        public SalesLineUpdatedEvent(Guid id, Guid idSales, Guid idItem, string discName, int quantity, decimal priceUnit, decimal cashback)
        {
            Id = id;
            IdSales = idSales;
            IdItem = idItem;
            DiscName = discName;
            Quantity = quantity;
            PriceUnit = priceUnit;
            SalesPrice = Quantity * PriceUnit;
            Cashback = cashback;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public Guid IdSales { get; private set; }

        public Guid IdItem { get; private set; }

        public string DiscName { get; set; }

        public int Quantity { get; set; }

        public decimal PriceUnit { get; set; }

        public decimal SalesPrice { get; private set; }

        public decimal Cashback { get; set; }
    }
}
