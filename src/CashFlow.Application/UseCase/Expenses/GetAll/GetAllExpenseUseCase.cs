

using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCase.Expenses.GetAll
{
    public  class GetAllExpenseUseCase : IGetAllExpenseUseCase
    {
        private  readonly IExpensesReadOnlyRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetAllExpenseUseCase(IExpensesReadOnlyRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }


        public async Task<ResponseExpenseJson> Execute()
        {
           var result =    await _expenseRepository.GetAll();

           return new ResponseExpenseJson
           {
              Expense = _mapper.Map<List<ResponseShortExpenseJson>>(result)
           };
        }
    }
}
