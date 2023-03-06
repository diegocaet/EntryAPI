using Application.Interfaces;
using MediatR;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class GetAllAccountsUseCaseHandler : IRequestHandler<GetAllAccountsUseCaseRequest, GetAllAccountsUseCaseResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAllAccountsUseCaseHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<GetAllAccountsUseCaseResponse> Handle(GetAllAccountsUseCaseRequest request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.GetAll();
            if (result == null || !result.Any()) 
            {
                return null;
            }

            return new GetAllAccountsUseCaseResponse(result);

        }
    }
}
