using CashFlow.Communication.Response;
using CashFlow.Exception;
using CashFlow.Exception.ExecptionsBase;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
           if(context.Exception is  CashFlowException)
            {
                HandlePrivationException(context);
            }
           else
            {
                ThrowUnknowError( context);
            }
        }


        private void HandlePrivationException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationExcption)
            {

                var ex = (ErrorOnValidationExcption)context.Exception;

                var errrosResponse = new ResponseErrorJson(ex.errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errrosResponse);

            }else
            {
                var errrosResponse = new ResponseErrorJson(context.Exception.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errrosResponse);
            }
        }

        private void ThrowUnknowError(ExceptionContext context)
        {
            var errorResponse  = new  ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; 
            context.Result = new  ObjectResult(errorResponse);

        }

    }
}
