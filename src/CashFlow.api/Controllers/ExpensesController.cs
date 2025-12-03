using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {


        [HttpPost]
        public IActionResult CreateExpense(
            [FromServices] IRegisterExpenseUseCase registerExpenseUseCase,
            [FromBody] RequestRegisterExpenseJson request
            )
        {
            var response =  registerExpenseUseCase.Execute(request);
            return Created(string.Empty, response);

        }

     
    }
}
