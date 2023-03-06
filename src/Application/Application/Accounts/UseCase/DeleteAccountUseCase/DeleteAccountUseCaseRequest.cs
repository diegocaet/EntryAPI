using MediatR;
namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class DeleteAccountUseCaseRequest : IRequest<DeleteAccountUseCaseResponse>
    {
        public DeleteAccountUseCaseRequest(string? parentAccount, int sequence)
        {
            ParentAccount = parentAccount;
            Sequence = sequence;
        }

        public string ParentAccount { get; set; }
        public int Sequence { get; set; }

    }
}
