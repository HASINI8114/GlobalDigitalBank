using gdb.Logging;
using GDB.Core.Application.Dtos;
using GDB.Core.Application.Services.Contracts;
using GDB.Core.Domain.Enums;
using GDB.Core.Domain.Exceptions;
using GDB.Core.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDB.Core.Infrastructure.Repositories.Contracts;

namespace GDB.Core.Application.Services.Implementations
{
    public class WithdrawTransactionCommand
        : ITransactionCommand<WithdrawResponseDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        private static readonly ILogger _logger =
            AppLogger.CreateLogger<WithdrawTransactionCommand>();

        public WithdrawTransactionCommand(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<WithdrawResponseDto> ExecuteAsync(
            TransactionDto transactionDto)
        {
            IAccount account =
                await _accountRepository.GetAccountAsync(
                    transactionDto.AccountNumber);

            if (account == null)
            {
                _logger.LogWarning(
                    "Withdraw failed: account {AccountNumber} not found",
                    transactionDto.AccountNumber);

                throw new AccountException(
                    "Account not found");
            }

            // Domain handles PIN,
            // amount validation,
            // balance validation,
            // account-specific withdrawal rules.
            account.Withdraw(
                transactionDto.Amount,
                transactionDto.Pin);

            // Update balance
            _accountRepository.UpdateBalance(
                transactionDto.AccountNumber,
                account.Balance);

            // Save transaction
            _transactionRepository.SaveTransaction(
                transactionDto.AccountNumber,
                null,
                TransactionType.Withdraw,
                transactionDto.Amount,
                TransactionStatus.Success,
                account.Balance,
                0);

            _logger.LogInformation(
    "Withdrew {Amount} from {AccountNumber}",
    transactionDto.Amount.ToString("C", new CultureInfo("en-IN")),
    transactionDto.AccountNumber);

            return new WithdrawResponseDto
            {
                Balance = account.Balance,
                TransactionStat = TransactionStatus.Success
            };
        }
    }
}
