using BeBlueApi.Domain.Validations;
using System;

namespace BeBlueApi.Domain.Commands
{
    public class RegisterNewDiscMusicCommand : DiscMusicCommand
    {
        public RegisterNewDiscMusicCommand(string name, Guid idGender, decimal price)
        {
            Name = name;
            IdGender = idGender;
            Price = price;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewDiscMusicCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
