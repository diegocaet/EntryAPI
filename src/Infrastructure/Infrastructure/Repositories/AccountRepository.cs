using Application.Accounts.UseCase.GetAccountsUseCase;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task Delete(string? parentAccount, int sequence)
        {
            using (SqlConnection connection = new SqlConnection(
                "Server=127.0.0.1;Database=CONTA;User Id=sa;Password=C@modor0;"))
            {
                await connection.ExecuteAsync(@"DELETE FROM CONTA 
                                                       WHERE ContaPai = @parentAccount
                                                         AND Sequencia = @sequence",
                                                         new { parentAccount, sequence });
            }
        }

        public async Task<Account> Get(string? parentAccount, int sequence)
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
                                                       WHERE c.ContaPai = @parentAccount
                                                         AND c.Sequencia = @sequence",
                                                         new { parentAccount, sequence});
            }

            return accountData;
        }

        public async Task<IEnumerable<Account>> GetAll()
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
                                                       ORDER BY Codigo ASC");
            }

            return accountData;
        }

        public async Task<int> GetNext(string? parentAccount)
        {
            int nextSequence;
            using (SqlConnection connection = new SqlConnection(
                "Server=127.0.0.1;Database=CONTA;User Id=sa;Password=C@modor0;"))
            {
                nextSequence = connection.QuerySingle<int>(@"SELECT max(Sequencia) + 1
                                                        FROM CONTA c
                                                       WHERE c.ContaPai = @parentAccount",
                                                         new { parentAccount });
            }

            return nextSequence;
        }

        public async Task InsertOrUpdate(InsertAccountUseCaseRequest account)
        {
            
            using (SqlConnection connection = new SqlConnection(
                "Server=127.0.0.1;Database=CONTA;User Id=sa;Password=C@modor0;"))
            {
                connection.Execute(@"IF NOT EXISTS(SELECT 1 FROM Conta c WHERE c.ContaPai = @parentAccount AND c.Sequencia = @sequence)
	                                                    INSERT INTO Conta VALUES (@Sequence, @ParentAccount, @Name, @Code, @Type, @AcceptEntry)
                                                    ELSE
	                                                    UPDATE Conta SET
		                                                    Nome = @Name,
		                                                    Codigo = @Code,
		                                                    Tipo = @Type,
		                                                    AceitaLancamento = @AcceptEntry
	                                                    WHERE ContaPai = @parentAccount AND Sequencia = @sequence",
                                                         new { account.ParentAccount, account.Sequence, account.Name, account.Code, account.AcceptEntry, account.Type });
            }

        }
    }
}
