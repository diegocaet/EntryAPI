using Application.Interfaces;
using MediatR;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class InsertAccountUseCaseHandler : IRequestHandler<InsertAccountUseCaseRequest, InsertAccountUseCaseResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public InsertAccountUseCaseHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<InsertAccountUseCaseResponse> Handle(InsertAccountUseCaseRequest request, CancellationToken cancellationToken)
        {
            await _accountRepository.InsertOrUpdate(request);

            return new InsertAccountUseCaseResponse();

        }
    }
}
