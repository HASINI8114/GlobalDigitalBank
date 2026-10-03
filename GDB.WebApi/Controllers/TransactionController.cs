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
     [FromBody] DepositRequestDto request)
        {
            var transactionDto = new TransactionDto
            {
                AccountNumber = request.AccountNumber,
                Amount = request.Amount
            };

            var response =
                await _transactionService
                    .ProcessTransactionAsync<DepositResponseDto>(
                        transactionDto,
                        TransactionType.Deposit);

            return Ok(response);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequestDto request)
        {
            var transactionDto = new TransactionDto
            {
                AccountNumber = request.AccountNumber,
                Pin = request.Pin,
                Amount = request.Amount
            };

            var response =
                await _transactionService
                    .ProcessTransactionAsync<WithdrawResponseDto>(
                        transactionDto,
                        TransactionType.Withdraw);

            return Ok(response);
        }


        [HttpPost("transfer")]
        public async Task<IActionResult> TransferFunds(
            [FromBody] TransferRequestDto request)
        {
            var transactionDto = new TransactionDto
            {
                FromAccount = request.FromAccount,
                ToAccount = request.ToAccount,
                Pin = request.Pin,
                Amount = request.Amount
            };

            var response =
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