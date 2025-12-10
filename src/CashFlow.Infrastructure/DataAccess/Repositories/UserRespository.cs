using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class UserRespository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {

        private readonly CashFlowDbContext _dbContext;

        public UserRespository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> ExistsActiveUserWithEmail(string email)
        {
           return await _dbContext.Users.AnyAsync(u => u.Email == email);

        }
        public async Task Add(Domain.Entities.User user)
        {
           await _dbContext.Users.AddAsync(user);
        }

        public async Task<User?> GetUserByEmail(string email)
        {

            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));

        }
    }
}
