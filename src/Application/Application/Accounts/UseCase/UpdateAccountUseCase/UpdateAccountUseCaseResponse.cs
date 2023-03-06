using Domain.Entities;
using Domain.Enums;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class UpdateAccountUseCaseResponse
    {
        public UpdateAccountUseCaseResponse(IEnumerable<Account> accounts)
        {
            Accounts = accounts;
        }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
