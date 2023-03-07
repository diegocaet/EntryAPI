using Domain.Enums;
using MediatR;
namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class InsertOrUpdateAccountUseCaseRequest : IRequest<InsertOrUpdateAccountUseCaseResponse>
    {
        public int Sequence { get; set; }
        public string ParentAccount { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public bool AcceptEntry { get; set; }
    }
}
