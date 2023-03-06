using Application.Interfaces;
using MediatR;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class UpdateAccountUseCaseHandler : IRequestHandler<UpdateAccountUseCaseRequest, UpdateAccountUseCaseResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public UpdateAccountUseCaseHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<UpdateAccountUseCaseResponse> Handle(UpdateAccountUseCaseRequest request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.GetAll();
            if (result == null || !result.Any()) 
            {
                return null;
            }

            return new UpdateAccountUseCaseResponse(result);

        }
    }
}
