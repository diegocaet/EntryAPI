using Domain.Entities;
using Domain.Enums;

namespace Application.Accounts.UseCase.GetNextAccountCodeUseCase
{
    public class GetNextAccountCodeUseCaseResponse
    {
        public GetNextAccountCodeUseCaseResponse(int next, string nextCode)
        {
            NextSequence = next;
            NextCode = nextCode;
        }
        public int NextSequence { get; set; }
        public string NextCode { get; set; }

    }
}
