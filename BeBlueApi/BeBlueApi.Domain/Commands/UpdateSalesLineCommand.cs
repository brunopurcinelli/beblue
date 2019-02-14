using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class UpdateSalesLineCommand : SalesLineCommand
    {
        public UpdateSalesLineCommand(Guid id, Guid idSales, Guid idItem, string discName, int quantity, decimal priceUnit, decimal cashback)
        {
            Id = id;
            IdSales = idSales;
            IdItem = idItem;
            DiscName = discName;
            Quantity = quantity;
            PriceUnit = priceUnit;
            SalesPrice = Quantity * PriceUnit;
            Cashback = Cashback;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateSalesLineCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
