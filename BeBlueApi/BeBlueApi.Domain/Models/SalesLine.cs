using BeblueApi.Domain.Core.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeBlueApi.Domain.Models
{
    public class SalesLine : Entity
    {
        public SalesLine(Guid id, Guid idSales, Guid idItem, string discName, int quantity, decimal priceUnit, decimal cashback)
        {
            Id = id;
            IdSales = idSales;
            IdItem = idItem;
            DiscName = discName;
            Quantity = quantity;
            PriceUnit = priceUnit;
            SalesPrice = Quantity * PriceUnit;
            Cashback = cashback;
        }

        // Empty constructor for EF
        protected SalesLine() { }

        public Guid IdSales { get; private set; }

        public Guid IdItem { get; private set; }

        public string DiscName { get; set; }

        public int Quantity { get; set; }

        public decimal PriceUnit { get; set; }

        public decimal SalesPrice { get; private set; }

        public decimal Cashback { get; set; }


        [ForeignKey("IdSales")]
        public virtual Sales Sales { get; set; }

        [ForeignKey("IdItem")]
        public virtual DiscMusic DiscMusic{ get; set; }
    }
}
