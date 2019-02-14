using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RemoveMusicGenderCommandValidation : MusicGenderValidation<RemoveMusicGenderCommand>
    {
        public RemoveMusicGenderCommandValidation()
        {
            ValidateId();
        }
    }
}
