

using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpenseRepository : IExpenseRepository
    {
        private readonly CashFlowDbContext _dbContext;

        public ExpenseRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Expense expense)
        {
            _dbContext .Expenses.Add(expense);
        }
    }
}
