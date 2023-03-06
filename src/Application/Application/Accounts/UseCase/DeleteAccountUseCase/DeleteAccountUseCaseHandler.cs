using Application.Interfaces;
using MediatR;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class DeleteAccountUseCaseHandler : IRequestHandler<DeleteAccountUseCaseRequest, DeleteAccountUseCaseResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public DeleteAccountUseCaseHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<DeleteAccountUseCaseResponse> Handle(DeleteAccountUseCaseRequest request, CancellationToken cancellationToken)
        {
            await _accountRepository.Delete(request.ParentAccount, request.Sequence);
            
            return new DeleteAccountUseCaseResponse();

        }
    }
}
