using GDB.Core.Application.Dtos;
using GDB.Core.Application.Services.Contracts;
using GDB.Core.Domain.Enums;
using gdb.Logging;
using Microsoft.Extensions.Logging;

namespace GDB.Core.Application.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private static readonly ILogger _logger =
            AppLogger.CreateLogger<TransactionService>();

        private readonly TransactionCommandFactory _transactionCommandFactory;

        public TransactionService(
            TransactionCommandFactory transactionCommandFactory)
        {
            _transactionCommandFactory = transactionCommandFactory;
        }

        public async Task<TResponse> ProcessTransactionAsync<TResponse>(
            TransactionDto transactionDto,
            TransactionType transactionType)
        {
            _logger.LogInformation(
                "Processing transaction {TransactionType}",
                transactionType);

            ITransactionCommand<TResponse> command =
    _transactionCommandFactory.Create<TResponse>(transactionType);

            TResponse response =
                await command.ExecuteAsync(transactionDto);

            _logger.LogInformation(
                "Transaction {TransactionType} completed successfully",
                transactionType);

            return response;
        }
    }
}
