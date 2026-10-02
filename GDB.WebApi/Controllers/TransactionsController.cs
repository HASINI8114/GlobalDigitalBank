using Microsoft.AspNetCore.Mvc;
using GDB.App.Application.Services.Contracts;
using GDB.App.Application.Dtos;
using GDB.App.Domain.Enums;

namespace GDB.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionQueryService _transactionQueryService;

        public TransactionsController(
            ITransactionService transactionService,
            ITransactionQueryService transactionQueryService)
        {
            _transactionService = transactionService;
            _transactionQueryService = transactionQueryService;
        }

        public class DepositRequest { public string AccountNumber { get; set; } = string.Empty; public decimal Amount { get; set; } }
        public class WithdrawRequest { public string AccountNumber { get; set; } = string.Empty; public string Pin { get; set; } = string.Empty; public decimal Amount { get; set; } }
        public class TransferRequest { public string FromAccountNumber { get; set; } = string.Empty; public string ToAccountNumber { get; set; } = string.Empty; public string Pin { get; set; } = string.Empty; public decimal Amount { get; set; } }

        [HttpPost("deposit")]
        public async Task<ActionResult<DepositResponseDto>> Deposit([FromBody] DepositRequest request)
        {
            if (request == null) return BadRequest();
            var dto = new TransactionDto { AccountNumber = request.AccountNumber, Amount = request.Amount };
            var resp = await _transactionService.ProcessTransactionAsync<DepositResponseDto>(dto, TransactionType.Deposit);
            return Ok(resp);
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult<WithdrawResponseDto>> Withdraw([FromBody] WithdrawRequest request)
        {
            if (request == null) return BadRequest();
            var dto = new TransactionDto { AccountNumber = request.AccountNumber, Pin = request.Pin, Amount = request.Amount };
            var resp = await _transactionService.ProcessTransactionAsync<WithdrawResponseDto>(dto, TransactionType.Withdraw);
            return Ok(resp);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult<TranferFundsResponseDto>> Transfer([FromBody] TransferRequest request)
        {
            if (request == null) return BadRequest();
            var dto = new TransactionDto { FromAccount = request.FromAccountNumber, ToAccount = request.ToAccountNumber, Pin = request.Pin, Amount = request.Amount };
            var resp = await _transactionService.ProcessTransactionAsync<TranferFundsResponseDto>(dto, TransactionType.Transfer);
            return Ok(resp);
        }

        [HttpGet("{accountNumber}/recent")]
        public async Task<ActionResult<List<ViewRecentTransactionsResponseDto>>> Recent(string accountNumber)
        {
            var resp = await _transactionQueryService.GetRecentTransactionsAsync(accountNumber);
            return Ok(resp);
        }
    }
}
