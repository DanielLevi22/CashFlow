using CashFlow.Application.UseCase.Users.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {



        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
        [FromServices]    IRegisterUserUseCase registerUserUseCase,
        [FromBody] RequestUserJson request

        )
        {
           var response = await registerUserUseCase.Execute(request);
           return Created(string.Empty, response);

        }

    }
}
