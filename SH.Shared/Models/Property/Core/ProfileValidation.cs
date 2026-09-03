using FluentValidation;

namespace SH.Shared.Models.Property.Core;

public class ProfileValidation : AbstractValidator<ProfileModel>
{
    public ProfileValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Nationality).NotEmpty();
        RuleFor(x => x.Age).NotEmpty();
        RuleFor(x => x.Gender).NotEmpty();
        RuleFor(x => x.Occupation).NotEmpty();
        RuleFor(x => x.HomeSchedule).NotEmpty();
        RuleFor(x => x.Languages).NotEmpty();
    }
}