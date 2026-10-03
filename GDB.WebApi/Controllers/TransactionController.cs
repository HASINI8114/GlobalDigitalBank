using GDB.Core.Application.Dtos;
using GDB.Core.Application.Services;
using GDB.Core.Application.Services.Contracts;
using GDB.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GDB.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionQueryService _transactionQueryService;

        public TransactionController(
    ITransactionService transactionService,
    ITransactionQueryService transactionQueryService)
        {
            _transactionService = transactionService;
            _transactionQueryService = transactionQueryService;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(
            string accountNumber, decimal amount)
        {
            TransactionDto transactionDto =
                new TransactionDto
                {
                    AccountNumber = accountNumber,
                    Amount = amount
                };

            DepositResponseDto response =
                await _transactionService
                    .ProcessTransactionAsync<DepositResponseDto>(
                        transactionDto,
                        TransactionType.Deposit);

            return Ok(response);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(
            string accountNumber, string pin, decimal amount)
        {
            TransactionDto transactionDto =
                new TransactionDto
                {
                    AccountNumber = accountNumber,
                    Pin = pin,
                    Amount = amount
                };

            WithdrawResponseDto response =
                await _transactionService
                    .ProcessTransactionAsync<WithdrawResponseDto>(
                        transactionDto,
                        TransactionType.Withdraw);

            return Ok(response);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferFunds(
            string fromAccountNumber,
            string toAccountNumber,
            string pin,
            decimal amount)
        {
            TransactionDto transactionDto =
                new TransactionDto
                {
                    FromAccount = fromAccountNumber,
                    ToAccount = toAccountNumber,
                    Pin = pin,
                    Amount = amount
                };

            TranferFundsResponseDto response =
                await _transactionService
                    .ProcessTransactionAsync<TranferFundsResponseDto>(
                        transactionDto,
                        TransactionType.Transfer);

            return Ok(response);
        }

        [HttpGet("{accountNumber}/recent")]
        public async Task<IActionResult> GetRecentTransactions(
            string accountNumber)
        {
            List<ViewRecentTransactionsResponseDto> response =
                await _transactionQueryService
                    .GetRecentTransactionsAsync(accountNumber);

            return Ok(response);
        }
    }
}