

using System.Net;

namespace CashFlow.Exception.ExecptionsBase
{
    public class ErrorOnValidationExcption : CashFlowException
    {
        private readonly List<string> _errors;

        public ErrorOnValidationExcption(List<string> errors) : base(string.Empty)
        {
            this._errors = errors;
        }

        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public override List<string> GetErros()
        {
           return _errors;
        }
    }
}
