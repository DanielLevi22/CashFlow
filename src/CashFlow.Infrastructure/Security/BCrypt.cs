using CashFlow.Domain.Security.Criptography;
using BC = BCrypt.Net.BCrypt;
namespace CashFlow.Infrastructure.Security
{
    public class BCrypt : IPasswordEncripter
    {

        public string Encript(string password)
        {
          return BC.HashPassword(password);
        }
    }
}
