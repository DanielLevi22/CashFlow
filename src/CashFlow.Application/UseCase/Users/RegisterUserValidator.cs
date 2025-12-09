
using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCase.Users
{
    public class RegisterUserValidator : AbstractValidator<RequestUserJson>
    {


        public RegisterUserValidator() { 
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required.");
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestUserJson>());

        }
    }
}
