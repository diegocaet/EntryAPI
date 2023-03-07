using Application.Accounts.UseCase.GetAccountsUseCase;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> Get(string? parentAccount, int sequence);
        Task<Account> GetByCode(string? parentAccount);
        Task<int> GetLastByParentAccount(string? parentAccount);
        Task<int> InsertOrUpdate(Account account);
        Task<int> Delete(string? parentAccount, int sequence);
    }
}
