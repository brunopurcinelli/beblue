using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RegisterNewSalesLineCommandValidation : SalesLineValidation<RegisterNewSalesLineCommand>
    {
        public RegisterNewSalesLineCommandValidation()
        {
            ValidateIdItem();
            ValidateIdSales();
            ValidateQuantity();
            ValidatePriceUnit();
            ValidateSalesPrice();
        }
    }
}
