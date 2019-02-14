using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RemoveSalesCommandValidation : SalesValidation<RemoveSalesCommand>
    {
        public RemoveSalesCommandValidation()
        {
            ValidateId();
        }
    }
}
