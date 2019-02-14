using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RemoveSalesLineCommandValidation : SalesLineValidation<RemoveSalesLineCommand>
    {
        public RemoveSalesLineCommandValidation()
        {
            ValidateId();
        }
    }
}
