using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class RegisterNewMusicGenderCommandValidation : MusicGenderValidation<RegisterNewMusicGenderCommand>
    {
        public RegisterNewMusicGenderCommandValidation()
        {
            ValidateDescription();
        }
    }
}
