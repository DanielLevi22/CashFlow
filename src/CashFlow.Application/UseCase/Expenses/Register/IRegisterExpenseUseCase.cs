using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Expenses.Register
{
    public interface IRegisterExpenseUseCase
    {

        Task<ResponseRegisteredExpenseJson> Execute(RequestRegisterExpenseJson request);
    }
}
 