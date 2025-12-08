
using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExecptionsBase;

namespace CashFlow.Application.UseCase.Expenses.Register
{
    public class RegisterExpenseUseCase: IRegisterExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterExpenseUseCase(
            IExpensesWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseRegisteredExpenseJson>  Execute(RequestExpenseJson request)
        {
            Validated(request);

            var entity = _mapper.Map<Expense>(request);


            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisteredExpenseJson>(entity);

        }

        private void Validated(RequestExpenseJson request)
        {
            var validator = new ExpenseValidator();
            var validationResult = validator.Validate(request);
        
            if (!validationResult.IsValid)
            {

                var errosMessages =  validationResult.Errors.Select(err => err.ErrorMessage).ToList();
                throw new ErrorOnValidationExcption(errosMessages);
            }
   
        }
    }

}
