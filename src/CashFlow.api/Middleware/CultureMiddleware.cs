using Microsoft.AspNetCore.Mvc.Routing;
using System.Globalization;

namespace CashFlow.Api.Middleware
{
    public class CultureMiddleware
    {

        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task  Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestCulture  = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            



            var cultureInfo = new CultureInfo("en-US");

            if (string.IsNullOrEmpty(requestCulture) == false && supportedLanguages.Exists(language => language.Name.Equals(requestCulture)))
            {
                cultureInfo = new System.Globalization.CultureInfo(requestCulture);
         
            }

            System.Globalization.CultureInfo.CurrentCulture = cultureInfo;
            System.Globalization.CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
