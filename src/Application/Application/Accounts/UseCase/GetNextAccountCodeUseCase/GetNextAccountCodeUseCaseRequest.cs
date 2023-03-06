using MediatR;
namespace Application.Accounts.UseCase.GetNextAccountCodeUseCase
{
    public class GetNextAccountCodeUseCaseRequest : IRequest<GetNextAccountCodeUseCaseResponse>
    {
        public GetNextAccountCodeUseCaseRequest(string? parentAccount)
        {
            ParentAccount = parentAccount;
        }

        public string ParentAccount { get; set; }
  
    }
}
