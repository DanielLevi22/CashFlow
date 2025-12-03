
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExecptionsBase;

namespace CashFlow.Application.UseCase.Expenses.Register
{
    public class RegisterExpenseUseCase: IRegisterExpenseUseCase
    {
        private readonly IExpenseRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterExpenseUseCase(IExpenseRepository repository, IUnitOfWork unitOfWork )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validated(request);

            var entity = new Expense
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Amount = request.Amount,
                PaymentType = (Domain.Enums.PaymentType)(PaymentType)request.PaymentType
            };


            _repository.Add(entity);

            _unitOfWork.Commit();

            return new ResponseRegisteredExpenseJson();
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
