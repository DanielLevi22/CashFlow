using CashFlow.Application.UseCase.Expenses.Delete;
using CashFlow.Application.UseCase.Expenses.GetAll;
using CashFlow.Application.UseCase.Expenses.GetById;
using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Application.UseCase.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ExpensesController : ControllerBase
    {


        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateExpense(
            [FromServices] IRegisterExpenseUseCase registerExpenseUseCase,
            [FromBody] RequestExpenseJson request
            )
        {
            var response = await registerExpenseUseCase.Execute(request);
            return Created(string.Empty, response);

        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllExpenses(
            [FromServices] IGetAllExpenseUseCase getAllExpenseUseCase
            )
        {
            var response = await getAllExpenseUseCase.Execute();
            if (response.Expense.Count != 0)
                return Ok(response);


            return NoContent();
        }


        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExpenseById(
            [FromServices] IGetExpenseByIdUseCase getExpenseByIdUseCase,
            [FromRoute] long id
            )
        {
            var response = await getExpenseByIdUseCase.Execute(id);
            return Ok(response);
        }


        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteExpenseById(
            [FromServices] IDeleteExpenseUseCase deleteExpenseUseCase,
            [FromRoute] long id
            )
        {
            await deleteExpenseUseCase.Execute(id);
            return NoContent();

        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateExpenseById(
            [FromServices] IUpdateExpenseUseCase updateExpense,
            [FromBody] RequestExpenseJson request,
            [FromRoute] long id
            )
        {
            await updateExpense.Execute(id,request);
            return NoContent();
        }
    }
}
