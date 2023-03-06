using Application.Accounts.UseCase.GetAccountsUseCase;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> Get(string? parentAccount, int sequence);
        Task<IEnumerable<Account>> GetAll();
        Task<int> GetNext(string? parentAccount);
        Task InsertOrUpdate(InsertAccountUseCaseRequest account);
        Task Delete(string? parentAccount, int sequence);
    }
}
