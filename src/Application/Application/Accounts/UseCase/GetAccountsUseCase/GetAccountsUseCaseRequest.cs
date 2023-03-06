using MediatR;
namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class GetAccountsUseCaseRequest : IRequest<GetAccountsUseCaseResponse>
    {
        public GetAccountsUseCaseRequest(string? parentAccount, int sequence)
        {
            ParentAccount = parentAccount;
            Sequence = sequence;
        }

        public string ParentAccount { get; set; }
        public int Sequence { get; set; }
    }
}
