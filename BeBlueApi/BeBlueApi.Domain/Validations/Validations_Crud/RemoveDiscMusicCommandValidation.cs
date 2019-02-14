using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RemoveDiscMusicCommandValidation : DiscMusicValidation<RemoveDiscMusicCommand>
    {
        public RemoveDiscMusicCommandValidation()
        {
            ValidateId();
        }
    }
}
