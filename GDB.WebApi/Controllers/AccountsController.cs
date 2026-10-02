using Microsoft.AspNetCore.Mvc;
using GDB.App.Application.Services.Contracts;
using GDB.App.Application.Dtos;

namespace GDB.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ViewAllAccountsResponseDto>> GetAll()
        {
            var accounts = _accountService.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpGet("{accountNumber}")]
        public async Task<ActionResult<ViewAccountResponseDto>> Get(string accountNumber)
        {
            var account = await _accountService.ViewAccountAsync(accountNumber);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpGet("{accountNumber}/balance")]
        public async Task<ActionResult<ViewBalanceResponseDto>> Balance(string accountNumber)
        {
            var balance = await _accountService.GetBalanceAsync(accountNumber);
            if (balance == null) return NotFound();
            return Ok(balance);
        }

        [HttpPost]
        public ActionResult<CreateAccountResponseDto> Create([FromBody] CreateAccountRequestDto request)
        {
            if (request == null) return BadRequest();
            var response = _accountService.CreateAccount(request);
            return Ok(response);
        }

        [HttpPost("close")]
        public async Task<ActionResult<CloseAccountResponseDto>> Close([FromBody] CloseAccountRequestDto request)
        {
            if (request == null) return BadRequest();
            var response = await _accountService.CloseAccountAsync(request);
            return Ok(response);
        }
    }
}
