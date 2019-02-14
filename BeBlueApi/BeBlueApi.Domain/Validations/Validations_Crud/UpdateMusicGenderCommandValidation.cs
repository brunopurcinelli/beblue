using BeBlueApi.Domain.Commands;

namespace BeBlueApi.Domain.Validations
{
    public class UpdateMusicGenderCommandValidation : MusicGenderValidation<UpdateMusicGenderCommand>
    {
        public UpdateMusicGenderCommandValidation()
        {
            ValidateId();
        }
    }
}
