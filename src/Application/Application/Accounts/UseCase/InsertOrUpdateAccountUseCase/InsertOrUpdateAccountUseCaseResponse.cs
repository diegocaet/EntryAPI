namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class InsertOrUpdateAccountUseCaseResponse
    {
        public InsertOrUpdateAccountUseCaseResponse(int rowsAffected)
        {
            RowsAffected = rowsAffected;
        }

        public int RowsAffected { get; set; }
    }
}
