
namespace CashFlow.Communication.Response
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessage { get; set; }

        public ResponseErrorJson(string errorMessage)
        {
            this.ErrorMessage = [errorMessage];
        }

        public ResponseErrorJson(List <string> errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
