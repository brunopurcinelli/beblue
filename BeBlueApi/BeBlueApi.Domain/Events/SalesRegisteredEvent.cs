using BeblueApi.Domain.Core.Events;
using System;

namespace BeBlueApi.Domain.Events
{
    public class SalesRegisteredEvent : Event
    {
        public SalesRegisteredEvent(Guid id, DateTime salesDate, decimal totalAmount, decimal totalCashback)
        {
            Id = id;
            SalesDate = salesDate;
            TotalAmount = totalAmount;
            TotalCashback = totalCashback;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public DateTime SalesDate { get; private set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalCashback { get; private set; }
    }
}
