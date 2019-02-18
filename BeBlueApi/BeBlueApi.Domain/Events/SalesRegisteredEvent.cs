using BeblueApi.Domain.Core.Events;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Domain.Events
{
    public class SalesRegisteredEvent : Event
    {
        public SalesRegisteredEvent(Guid id, DateTime salesDate, decimal totalAmount, decimal totalCashback, List<SalesLineRegisteredEvent> line)
        {
            Id = id;
            SalesDate = salesDate;
            TotalAmount = totalAmount;
            TotalCashback = totalCashback;
            SalesLine = line;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public DateTime SalesDate { get; private set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalCashback { get; private set; }

        public List<SalesLineRegisteredEvent> SalesLine { get; private set; }
    }
}
