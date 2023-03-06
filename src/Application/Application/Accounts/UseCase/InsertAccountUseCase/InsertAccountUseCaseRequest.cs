using Domain.Enums;
using MediatR;
namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class InsertAccountUseCaseRequest : IRequest<InsertAccountUseCaseResponse>
    {
        public int Sequence { get; set; }
        public string ParentAccount { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public AccountType Type { get; set; }
        public bool AcceptEntry { get; set; }
    }
}
