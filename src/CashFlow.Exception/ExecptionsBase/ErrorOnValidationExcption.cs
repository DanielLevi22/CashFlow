

namespace CashFlow.Exception.ExecptionsBase
{
    public class ErrorOnValidationExcption : CashFlowException
    {
        public List<string> errors;

        public ErrorOnValidationExcption(List<string> errors)
        {
            this.errors = errors;
        }
    }
}
