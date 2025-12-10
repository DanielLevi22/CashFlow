namespace CashFlow.Domain.Security.Criptography
{
    public interface IPasswordEncripter
    {
        string Encript(string password);
        public bool Verify(string Password, string PasswordHash);
    }
}
