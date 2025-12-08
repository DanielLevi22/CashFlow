

using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExecptionsBase;

namespace CashFlow.Application.UseCase.Expenses.GetById
{
    public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
    {


        readonly public IExpensesReadOnlyRepository _expenseRepository;
        readonly public IMapper _mapper;
        public GetExpenseByIdUseCase(IExpensesReadOnlyRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<ResponseExpenseJson?> Execute(long id)
        {
            var result = await _expenseRepository.GetById(id);


            if (result == null) {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            return _mapper.Map<ResponseExpenseJson>(result);
        }
    }
}
