using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {


        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof( ResponseErrorJson),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>  CreateExpense(
            [FromServices] IRegisterExpenseUseCase registerExpenseUseCase,
            [FromBody] RequestRegisterExpenseJson request
            )
        {
            var response =  await registerExpenseUseCase.Execute(request);
            return Created(string.Empty, response);

        }

     
    }
}
