using Application.Interfaces;
using MediatR;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class GetAccountsUseCaseHandler : IRequestHandler<GetAccountsUseCaseRequest, GetAccountsUseCaseResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountsUseCaseHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<GetAccountsUseCaseResponse> Handle(GetAccountsUseCaseRequest request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.Get(request.ParentAccount, request.Sequence);
            if (result == null) 
            {
                return null;
            }

            return new GetAccountsUseCaseResponse(result);

        }
    }
}
