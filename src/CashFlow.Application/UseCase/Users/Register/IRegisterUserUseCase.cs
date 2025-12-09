

using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Users.Register
{
    public interface IRegisterUserUseCase


    {
        public Task<ResponseRegisteredExpenseJson> Execute(RequestUserJson request);
    }
}
