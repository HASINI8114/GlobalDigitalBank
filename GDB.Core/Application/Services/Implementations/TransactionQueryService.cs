using GDB.Core.Application.Dtos;
using GDB.Core.Application.Services.Contracts;
using GDB.Core.Domain.Exceptions;
using GDB.Core.Domain.Models;
using GDB.App.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDB.Core.Infrastructure.Repositories.Contracts;
using GDB.Core.Infrastructure.Repositories;

namespace GDB.Core.Application.Services.Implementations
{
    public class TransactionQueryService
        : ITransactionQueryService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionQueryService(
    IAccountRepository accountRepository,
    ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }


        public async Task<List<ViewRecentTransactionsResponseDto>>
            GetRecentTransactionsAsync(
                string accountNumber)
        {
            IAccount account =
                await _accountRepository.GetAccountAsync(
                    accountNumber);

            if (account == null)
            {
                throw new AccountException(
                    "Account not found.");
            }

            return _transactionRepository
                .GetRecentTransactions(accountNumber);
        }
    }
}
