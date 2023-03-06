using Application.Interfaces;
using MediatR;

namespace Application.Accounts.UseCase.GetNextAccountCodeUseCase
{
    public class GetNextAccountCodeUseCaseHandler : IRequestHandler<GetNextAccountCodeUseCaseRequest, GetNextAccountCodeUseCaseResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public GetNextAccountCodeUseCaseHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<GetNextAccountCodeUseCaseResponse> Handle(GetNextAccountCodeUseCaseRequest request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.GetNext(request.ParentAccount);
            if (result == null) 
            {
                return null;
            }


            if (result == 10000)
            {
                return new GetNextAccountCodeUseCaseResponse(1, string.Format($"{ request.ParentAccount }.{1}"));
            }
            else
            {
                return new GetNextAccountCodeUseCaseResponse(result, string.Format($"{request.ParentAccount}.{result}"));
            }
        }
    }
}
