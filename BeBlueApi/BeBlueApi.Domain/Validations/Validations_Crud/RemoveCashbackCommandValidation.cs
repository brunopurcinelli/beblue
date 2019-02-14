using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RemoveCashbackCommandValidation : CashbackValidation<RemoveCashbackCommand>
    {
        public RemoveCashbackCommandValidation()
        {
            ValidateId();
        }
    }
}
