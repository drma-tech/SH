using FluentValidation;

namespace SH.Shared.Models.Property.Core;

public class PropertyValidation : AbstractValidator<PropertyModel>
{
    public PropertyValidation()
    {
        RuleSet("OFF", () =>
        {
            RuleFor(x => x.Address!.CountryName).NotEmpty()
                .WithMessage("An address is required initially.");

            RuleFor(x => x.Address!.State).NotEmpty()
                .WithName(Translations.Model.Property.State);

            RuleFor(x => x.Address!.City).NotEmpty()
                .WithName(Translations.Model.Property.City);

            RuleFor(x => x.Address!.Latitude).NotEmpty();

            RuleFor(x => x.Address!.Longitude).NotEmpty();

            RuleFor(x => x.Address!.Label).NotEmpty()
                .WithName(Translations.Model.Property.Label)
                .WithMessage("The label is missing. Please contact support.");

            RuleFor(x => x.Title).NotEmpty()
                .WithName(Translations.Model.Property.Title);

            RuleFor(x => x.Members).NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(2048)
                .Must(n => !StringHelper.IsLikelySpam(n)).WithMessage($"'{Translations.Model.Property.Description}' contains suspicious or spam-like content.")
                .WithName(Translations.Model.Property.Description);

            RuleFor(x => x.LocationDesc)
                .MaximumLength(2048)
                .Must(n => !StringHelper.IsLikelySpam(n)).WithMessage($"'{Translations.Model.Property.LocationDesc}' contains suspicious or spam-like content.")
                .WithName(Translations.Model.Property.LocationDesc);

            RuleFor(x => x.CheckInInstructions)
                .MaximumLength(2048)
                .Must(n => !StringHelper.IsLikelySpam(n)).WithMessage($"'{Translations.Model.Property.CheckInInstructions}' contains suspicious or spam-like content.")
                .WithName(Translations.Model.Property.CheckInInstructions);
        });

        RuleSet("ON", () =>
        {
        });

        //RuleFor(x => x.NickName)
        //    .NotEmpty()
        //    .MaximumLength(18).WithMessage("Nickname must be 18 characters or fewer.")
        //    .Must(n => !StringHelper.IsLikelySpam(n)).WithMessage("Nickname contains suspicious or spam-like content.")
        //    .WithName(ProfileBasicModel.NickName_Name);

        //RuleFor(x => x.Description)
        //    .MaximumLength(512).WithMessage("Description must be 512 characters or fewer.")
        //    .Must(d => !StringHelper.IsLikelySpam(d)).WithMessage("Description contains suspicious or spam-like content.")
        //    .WithName(ProfileBasicModel.Description_Name);

        //RuleFor(x => x.Nationality)
        //    .NotEmpty()
        //    .WithName("Nationality");

        //RuleFor(x => x.Location)
        //    .NotEmpty()
        //    .WithName(ProfileBasicModel.Location_Name);

        //RuleFor(x => x.Languages)
        //    .NotEmpty()
        //    .Must(value => value.Count <= 3)
        //    .WithMessage(string.Format(Validations.ChooseMaximumOptions, 3, ProfileBasicModel.Languages_Name))
        //    .WithName(ProfileBasicModel.Languages_Name);
    }
}