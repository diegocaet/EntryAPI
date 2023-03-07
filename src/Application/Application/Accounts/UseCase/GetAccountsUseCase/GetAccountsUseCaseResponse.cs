using Domain.Entities;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class GetAccountsUseCaseResponse
    {
        public GetAccountsUseCaseResponse(IEnumerable<Account> accounts)
        {
            Accounts = accounts;
        }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
