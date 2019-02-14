using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class UpdateDiscMusicCommandValidation : DiscMusicValidation<UpdateDiscMusicCommand>
    {
        public UpdateDiscMusicCommandValidation()
        {
            ValidateId();
        }
    }
}
