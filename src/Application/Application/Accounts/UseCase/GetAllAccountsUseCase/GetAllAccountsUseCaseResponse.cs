using Domain.Entities;
using Domain.Enums;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class GetAllAccountsUseCaseResponse
    {
        public GetAllAccountsUseCaseResponse(IEnumerable<Account> accounts)
        {
            Accounts = accounts;
        }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
