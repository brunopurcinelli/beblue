using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class UpdateSalesLineCommandValidation : SalesLineValidation<UpdateSalesLineCommand>
    {
        public UpdateSalesLineCommandValidation()
        {
            ValidateId();
        }
    }
}
