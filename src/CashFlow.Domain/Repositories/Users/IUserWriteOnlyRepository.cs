namespace CashFlow.Domain.Repositories.Users
{
    public interface IUserReadOnlyRepository
    {

        Task<bool> ExistsActiveUserWithEmail(string email);
    }
}
