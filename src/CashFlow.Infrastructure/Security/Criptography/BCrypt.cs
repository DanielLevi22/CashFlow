using CashFlow.Domain.Security.Criptography;
using BC = BCrypt.Net.BCrypt;
namespace CashFlow.Infrastructure.Security.Criptography
{
    public class BCrypt : IPasswordEncripter
    {

        public string Encript(string password)
        {
          return BC.HashPassword(password);
        }

        public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
    }
}
