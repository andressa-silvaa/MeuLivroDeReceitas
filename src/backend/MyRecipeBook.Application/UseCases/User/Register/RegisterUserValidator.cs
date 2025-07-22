using FluentValidation;
using MyRecipeBook.Comunication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
        RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY);
        When(user => string.IsNullOrEmpty(user.Email) == false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
        When(user => string.IsNullOrEmpty(user.Password) == false, () =>
        {
            RuleFor(user => user.Password.Length).LessThanOrEqualTo(8).WithMessage(ResourceMessagesException.PASSWORD_LIMIT_CHAR);
        });
    }
}
