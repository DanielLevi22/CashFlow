
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Exception.ExecptionsBase;

namespace CashFlow.Application.UseCase.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validated(request);
            return new ResponseRegisteredExpenseJson
            {
                Title = request.Title
            };

        }

        private void Validated(RequestRegisterExpenseJson request)
        {
            var validator = new RegisterExpenseValidator();
            var validationResult = validator.Validate(request);
        
            if (!validationResult.IsValid)
            {

                var errosMessages =  validationResult.Errors.Select(err => err.ErrorMessage).ToList();
                throw new ErrorOnValidationExcption(errosMessages);
            }
   
        }
    }

}
