
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCase.Login
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);

    }
}
