

using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCase.Expenses.Update
{
    public interface IUpdateExpenseUseCase
    {
        public  Task Execute(long id, RequestExpenseJson request);

    }
}
