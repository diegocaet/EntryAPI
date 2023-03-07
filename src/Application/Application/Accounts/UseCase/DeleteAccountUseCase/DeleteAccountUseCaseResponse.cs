﻿namespace Application.Accounts.UseCase.GetAccountsUseCase
{
    public class DeleteAccountUseCaseResponse
    {
        public DeleteAccountUseCaseResponse(int rowsAffected)
        {
            RowsAffected = rowsAffected;
        }

        public int RowsAffected { get; set; }
    }
}
