using System;

namespace BeBlueApi.Domain.Commands
{
    public class LineCommand
    {
        public LineCommand(Guid idDisc,int quantity, decimal priceUnit, decimal salesPrice, decimal cashback)
        {
            IdDisc = idDisc;
            Quantity = quantity;
            PriceUnit = priceUnit;
            SalesPrice = salesPrice;
            Cashback = cashback;
        }

        public Guid Id { get; protected set; }

        public Guid IdSales { get; protected set; }

        public Guid IdDisc { get; protected set; }

        public int Quantity { get; protected set; }

        public decimal PriceUnit { get; protected set; }

        public decimal SalesPrice { get; protected set; }

        public decimal Cashback { get; protected set; }
    }
}
