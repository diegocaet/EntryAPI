using Domain.Entities;
using Domain.Enums;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class GetAccountsUseCaseResponse
    {
        public GetAccountsUseCaseResponse(Account account)
        {
            Sequence = account.Sequence;
            ParentAccount = account.ParentAccount;
            Name = account.Name;
            Code = account.Code;
            Type = account.Type;
            AcceptEntry = account.AcceptEntry;
        }
        public int Sequence { get; set; }
        public string ParentAccount { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public AccountType Type { get; set; }
        public bool AcceptEntry { get; set; }
    }
}
