using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCase.Users
{
    public class PasswordValidator<T> : PropertyValidator<T, string>
    {


        private const string ERROR_MESSAGE_KEY = "ErrorMessage";
        public override string Name => "PasswordValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"{{{ERROR_MESSAGE_KEY}}}";
        }

        public override bool IsValid(ValidationContext<T> context, string password)
        {
           if(string.IsNullOrEmpty(password) || password.Length < 8)
           {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
                return false;
           }

           Regex hasUpperChar = new Regex(@"[A-Z]+");

            if (!hasUpperChar.IsMatch(password))
            {
                return false;
            }

            return true;
        }
    }
}
