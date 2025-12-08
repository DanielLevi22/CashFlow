

using System.Net;

namespace CashFlow.Exception.ExecptionsBase
{
    public class NotFoundException : CashFlowException
    {
          

        public NotFoundException(string message): base(message)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErros()
        {
            return [this.Message];
        }
    }
}
