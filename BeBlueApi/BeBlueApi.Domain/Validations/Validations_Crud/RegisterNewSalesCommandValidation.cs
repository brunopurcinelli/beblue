using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RegisterNewSalesCommandValidation : SalesValidation<RegisterNewSalesCommand>
    {
        public RegisterNewSalesCommandValidation()
        {
            ValidateSalesDate();
        }
    }
}
