using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class RegisterNewSalesLineCommand : SalesLineCommand
    {
        public RegisterNewSalesLineCommand(Guid idSales, Guid idItem, int quantity, decimal priceUnit, decimal cashback)
        {
            IdSales = idSales;
            IdDisc = idItem;
            Quantity = quantity;
            PriceUnit = priceUnit;
            SalesPrice = Quantity * PriceUnit;
            Cashback = cashback;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewSalesLineCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
