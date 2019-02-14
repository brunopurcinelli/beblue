using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class UpdateDiscMusicCommand : DiscMusicCommand
    {
        public UpdateDiscMusicCommand(Guid id, Guid idGender, string name, decimal price)
        {
            Id = id;
            IdGender = idGender;
            Price = price;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateDiscMusicCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
