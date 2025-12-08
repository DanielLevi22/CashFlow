
using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExecptionsBase;

namespace CashFlow.Application.UseCase.Expenses.Update
{
     public class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {

        private  readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private  readonly IExpensesUpdateOnlyRepository _repository;

        public UpdateExpenseUseCase(IMapper mapper, IUnitOfWork unitOfWork, IExpensesUpdateOnlyRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        public async Task Execute(long id, RequestExpenseJson request)
        {

            Validated(request);

           var expense = await _repository.GetById(id);
            if (expense is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

            }
            _mapper.Map(request, expense);

            _repository.Update(expense);

            await _unitOfWork.Commit();


        }


        private void Validated(RequestExpenseJson request)
        {
            var validator = new ExpenseValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {

                var errosMessages = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
                throw new ErrorOnValidationExcption(errosMessages);
            }

        }

    }
}
