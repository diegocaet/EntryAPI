using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class InsertOrUpdateAccountUseCaseHandler : IRequestHandler<InsertOrUpdateAccountUseCaseRequest, InsertOrUpdateAccountUseCaseResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public InsertOrUpdateAccountUseCaseHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<InsertOrUpdateAccountUseCaseResponse> Handle(InsertOrUpdateAccountUseCaseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Sequence > 999)
                    throw new HttpRequestException("Sequence inválida."); 

                int rows;

                var account = await _accountRepository.Get(request.ParentAccount, request.Sequence);

                if (!account.Any())
                {

                    var newAccount = new Account()
                    {
                        Sequence = request.Sequence,
                        ParentAccount = request.ParentAccount,
                        Name = request.Name,
                        Code = request.ParentAccount != string.Empty ? $"{request.ParentAccount}.{request.Sequence}" : request.Sequence.ToString(),
                        AcceptEntry = request.AcceptEntry,
                        Type = request.Type
                    };

                    if (request.ParentAccount != string.Empty)
                    {
                        var parentAccount = await _accountRepository.GetByCode(request.ParentAccount);

                        if (parentAccount == null)
                            throw new HttpRequestException("Conta pai não encontrada.");

                        if (parentAccount.AcceptEntry)
                            throw new HttpRequestException("Não é possível criar uma conta filha para um conta que aceita lançamento.");
                        
                        if (parentAccount.Type != request.Type)
                            throw new HttpRequestException("Não é possível criar uma conta filha com tipo diferente da conta pai.");

                        

                        newAccount.Type = parentAccount.Type;
                        //Deve proibir a criação com tipo diferente ou herdar do pai?
                    }

                    rows = await _accountRepository.InsertOrUpdate(newAccount);
                }
                else
                {
                    var childAccounts = await _accountRepository.Get($"{request.ParentAccount}.{request.Sequence}", 0);
                    if (childAccounts.Any())
                    {
                        if (request.AcceptEntry)
                        {
                            throw new HttpRequestException("Não é possível aceitar lançamento para uma conta que possui filhas.");
                        }
                        if (request.Type != childAccounts.First().Type)
                        {
                            throw new HttpRequestException("Não é possível alterar o tipo de uma conta que possui filhas.");
                        }
                    }

                    var updateAccount = new Account()
                    {
                        Sequence = request.Sequence,
                        ParentAccount = request.ParentAccount,
                        Name = request.Name,
                        Code = request.ParentAccount != string.Empty ? $"{request.ParentAccount}.{request.Sequence}" : request.Sequence.ToString(),
                        AcceptEntry = request.AcceptEntry,
                        Type = request.Type
                    };

                    rows = await _accountRepository.InsertOrUpdate(updateAccount);
                }

                return new InsertOrUpdateAccountUseCaseResponse(rows);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
