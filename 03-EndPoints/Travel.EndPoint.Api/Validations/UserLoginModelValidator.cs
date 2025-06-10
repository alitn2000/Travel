using FluentValidation;
using Travel.Domain.Core.Enums;
using Travel.EndPoint.Api.Models.UserModels;

namespace Travel.EndPoint.Api.Validations;

public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
{
    public UserLoginModelValidator()
    {
        RuleFor(x => x.UserName)
             .NotEmpty().WithMessage("Username is required.");

        When(x => x.UserNameType == UserNameEnum.Mobile, () =>
        {
            RuleFor(x => x.UserName)
            .Matches(@"^\+989\d{9}$").WithMessage("Mobile number must be in the format +989XXXXXXXXX.");
        });
        When(x => x.UserNameType == UserNameEnum.Email, () =>
        {
            RuleFor(x => x.UserName)
                .EmailAddress().WithMessage("Email Is Invalid!!!");
        });
    }
}
