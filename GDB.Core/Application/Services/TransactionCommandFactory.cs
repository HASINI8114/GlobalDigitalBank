using GDB.Core.Application.Services.Contracts;
using GDB.Core.Application.Services.Implementations;
using GDB.Core.Domain.Enums;
using GDB.Core.Infrastructure.Repositories.Contracts;
using System;

namespace GDB.Core.Application.Services
{
    public class TransactionCommandFactory
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionCommandFactory(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public ITransactionCommand<TResponse> Create<TResponse>(
            TransactionType transactionType)
        {
            return transactionType switch
            {
                TransactionType.Deposit =>
                    (ITransactionCommand<TResponse>)new DepositTransactionCommand(
                        _accountRepository,
                        _transactionRepository),

                TransactionType.Withdraw =>
                    (ITransactionCommand<TResponse>)new WithdrawTransactionCommand(
                        _accountRepository,
                        _transactionRepository),

                TransactionType.Transfer =>
                    (ITransactionCommand<TResponse>)new TransferTransactionCommand(
                        _accountRepository,
                        _transactionRepository),

                _ => throw new ArgumentException(
                    $"Invalid transaction type: {transactionType}")
            };
        }
    }
}