using BeblueApi.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace BeBlueApi.Domain.Models
{
    public class Sales : Entity
    {
        public Sales(Guid id, DateTime salesDate, decimal totalAmount, decimal totalCashback)
        {
            Id = id;
            SalesDate = salesDate;
            TotalAmount = totalAmount;
            TotalCashback = totalCashback;
        }

        // Empty constructor for EF
        protected Sales() { }

        public DateTime SalesDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalCashback { get;  set; }


        public ICollection<SalesLine> Lines { get; set; }
    }
}
