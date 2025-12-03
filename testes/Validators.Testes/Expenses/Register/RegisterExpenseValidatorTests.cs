
using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Xunit.Sdk;

namespace Validators.Testes.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {

        [Fact]
        public void Sucess()
        {


            var validator = new RegisterExpenseValidator();


            var request = RequestRegisterExpenseJsonBuilder.Builder();



            var result = validator.Validate(request);


            result.IsValid.Should().BeTrue();
        }



        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
         
        public void Error_Title_Empty(string title)
        {


            var validator = new RegisterExpenseValidator();


            var request = RequestRegisterExpenseJsonBuilder.Builder();
               
            request.Title = title;


            var result = validator.Validate(request);


            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_RIQUIRED));
        }



        [Fact]
        public void Error_Date_Future()
        {


            var validator = new RegisterExpenseValidator();


            var request = RequestRegisterExpenseJsonBuilder.Builder();

            request.Date = DateTime.UtcNow.AddDays(5);

            var result = validator.Validate(request);


            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE));
        }

        [Fact]
        public void Error_Payment_Type_Invalid()
        {


            var validator = new RegisterExpenseValidator();


            var request = RequestRegisterExpenseJsonBuilder.Builder();

            request.PaymentType = (PaymentType)5;

            var result = validator.Validate(request);


            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-7)]
        public void Error_Amount_Invalid(decimal amount)
        {


            var validator = new RegisterExpenseValidator();


            var request = RequestRegisterExpenseJsonBuilder.Builder();

            request.Amount = amount;

            var result = validator.Validate(request);


            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATHER_THAN_ZERO));
        }

    }
}
