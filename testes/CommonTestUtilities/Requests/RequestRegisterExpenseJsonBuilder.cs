
using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterExpenseJsonBuilder
    {

        public static RequestExpenseJson Builder() {

         return  new Faker<RequestExpenseJson>()
                .RuleFor(r => r.Amount, f => f.Finance.Amount())
                .RuleFor(r => r.Date, f => f.Date.Past())
                .RuleFor(r => r.Description, f => f.Lorem.Sentence())
                .RuleFor(r => r.Title, f => f.Commerce.Product())
                .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentType>());
        }
    }
}
