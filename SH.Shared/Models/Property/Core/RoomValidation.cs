using FluentValidation;

namespace SH.Shared.Models.Property.Core;

public class RoomValidation : AbstractValidator<RoomModel>
{
    public RoomValidation()
    {
        RuleFor(x => x.Title).NotEmpty()
            .WithName(Translations.Model.Room.Title);

        RuleFor(x => x.Description)
            .MaximumLength(2048)
            .Must(n => !StringHelper.IsLikelySpam(n)).WithMessage($"'{Translations.Model.Room.Description}' contains suspicious or spam-like content.")
            .WithName(Translations.Model.Room.Description);

        RuleFor(x => x.Costs).NotEmpty()
           .WithName(Translations.Model.Room.RentalTerms);

        //RuleFor(x => x.Photos).NotEmpty()
        //    .WithName(Translations.Model.Property.Photos);
    }
}