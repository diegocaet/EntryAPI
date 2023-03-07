using Application.Accounts.UseCase.GetAccountsUseCase;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<int> Delete(string? parentAccount, int sequence)
        {
            int rows;
            using (SqlConnection connection = new SqlConnection(
                "Server=127.0.0.1;Database=CONTA;User Id=sa;Password=C@modor0;"))
            {
                rows = await connection.ExecuteAsync(@"DELETE FROM CONTA 
                                                       WHERE ContaPai = @parentAccount
                                                         AND Sequencia = @sequence",
                                                         new { parentAccount, sequence });

            }
            return rows;
        }

        public async Task<IEnumerable<Account>> Get(string? parentAccount, int sequence)
        {
            IEnumerable<Account> accountData;
            using (SqlConnection connection = new SqlConnection(
                "Server=127.0.0.1;Database=CONTA;User Id=sa;Password=C@modor0;"))
            {
                accountData = connection.Query<Account>(@"SELECT Sequencia 'Sequence'
                                                            ,ContaPai 'ParentAccount'
                                                            ,Nome 'Name'
                                                            ,Codigo 'Code'
                                                            ,Tipo 'Type'
                                                            ,AceitaLancamento 'AcceptEntry'
                                                        FROM CONTA c
                                                       WHERE (c.ContaPai = @parentAccount or @parentAccount is null)
                                                         AND (c.Sequencia = @sequence or @sequence = 0)
                                                       ORDER BY Codigo ASC",
                                                         new { parentAccount, sequence });
            }

            return accountData;
        }

        public async Task<Account> GetByCode(string? code)
        {
            try
            {
                Account accountData;
                using (SqlConnection connection = new SqlConnection(
                    "Server=127.0.0.1;Database=CONTA;User Id=sa;Password=C@modor0;"))
                {
                    accountData = connection.QuerySingle<Account>(@"SELECT Sequencia 'Sequence'
                                                            ,ContaPai 'ParentAccount'
                                                            ,Nome 'Name'
                                                            ,Codigo 'Code'
                                                            ,Tipo 'Type'
                                                            ,AceitaLancamento 'AcceptEntry'
                                                        FROM CONTA c
                                                       WHERE c.Codigo = @code",
                                                             new { code });
                }

                return accountData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> GetLastByParentAccount(string? parentAccount)
        {
            int nextSequence;
            using (SqlConnection connection = new SqlConnection(
                "Server=127.0.0.1;Database=CONTA;User Id=sa;Password=C@modor0;"))
            {
                nextSequence = connection.QuerySingle<int>(@"SELECT max(Sequencia)
                                                        FROM CONTA c
                                                       WHERE c.ContaPai = @parentAccount",
                                                         new { parentAccount });
            }

            return nextSequence;
        }

        public async Task<int> InsertOrUpdate(Account account)
        {
            int rows;
            using (SqlConnection connection = new SqlConnection(
                "Server=127.0.0.1;Database=CONTA;User Id=sa;Password=C@modor0;"))
            {
                rows = connection.Execute(@"IF NOT EXISTS(SELECT 1 FROM Conta c WHERE c.ContaPai = @parentAccount AND c.Sequencia = @sequence)
	                                                    INSERT INTO Conta VALUES (@Sequence, @ParentAccount, @Name, @Code, @Type, @AcceptEntry)
                                                    ELSE
	                                                    UPDATE Conta SET
		                                                    Nome = @Name,
		                                                    Codigo = @Code,
		                                                    Tipo = @Type,
		                                                    AceitaLancamento = @AcceptEntry
	                                                    WHERE ContaPai = @parentAccount AND Sequencia = @sequence",
                                                         new
                                                         {
                                                             ParentAccount = account.ParentAccount,
                                                             Sequence = account.Sequence,
                                                             Name = account.Name,
                                                             Code = account.Code,
                                                             AcceptEntry = account.AcceptEntry,
                                                             Type = account.Type
                                                         });
            }

            return rows;

        }
    }
}
