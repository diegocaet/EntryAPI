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
            try
            {
                var result = await _accountRepository.GetLastByParentAccount(request.ParentAccount);
                if (result == 0)
                {
                    return null;
                }

                if (result == 999)
                {
                    var parentSequences = request.ParentAccount.Split(".");
                    
                    for (var i = parentSequences.Length; i >= 0; i--)
                    {
                        if (i == 0)
                        {
                            var lastSequence = await _accountRepository.GetLastByParentAccount("");
                            if (lastSequence < 999)
                            {
                                var filho = await _accountRepository.GetLastByParentAccount(lastSequence.ToString());
                                if (filho < 999)
                                    return new GetNextAccountCodeUseCaseResponse(filho + 1, string.Format($"{lastSequence}.{filho + 1}"));

                                return new GetNextAccountCodeUseCaseResponse(lastSequence + 1, string.Format($"{lastSequence + 1}"));
                            }
                        }

                        if (parentSequences[i - 1] != "999")
                        {
                            string newSequence = "";

                            for (var n = 0; n < i; n++)
                                newSequence += parentSequences[n];

                            var lastSequence = await _accountRepository.GetLastByParentAccount(newSequence);

                            if (lastSequence < 999)
                            {
                                var filho = await _accountRepository.GetLastByParentAccount(lastSequence.ToString());
                                if (filho < 999)
                                    return new GetNextAccountCodeUseCaseResponse(filho + 1, string.Format($"{newSequence}.{filho + 1}"));

                                return new GetNextAccountCodeUseCaseResponse(lastSequence + 1, string.Format($"{newSequence}.{lastSequence + 1}"));
                            }

                        }
                    }
                }
                else
                {
                    int nexSequence = result + 1;
                    return new GetNextAccountCodeUseCaseResponse(nexSequence, string.Format($"{request.ParentAccount}.{nexSequence}"));
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
