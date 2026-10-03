using GDB.Core.Application.Dtos;
using GDB.Core.Application.Services;
using GDB.Core.Application.Services.Contracts;
using GDB.Core.Application.Services.Implementations;
using GDB.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GDB.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{accNo}")]
        public async Task<IActionResult> GetAccount(string accNo)
        {
            IAccount account = await _accountService.GetAccountAsync(accNo);
            return Ok(account);
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            return Ok(_accountService.GetAllAccounts());
        }

        [HttpGet("{accNo}/balance")]
        public async Task<IActionResult> GetBalance(string accNo)
        {
            ViewBalanceResponseDto response = await _accountService.GetBalanceAsync(accNo);
            return Ok(response);
        }

        [HttpGet("{accNo}/details")]
        public async Task<IActionResult> ViewAccount(string accNo)
        {
            ViewAccountResponseDto response = await _accountService.ViewAccountAsync(accNo);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateAccount(CreateAccountRequestDto request)
        {
            CreateAccountResponseDto response = _accountService.CreateAccount(request);
            return Ok(response);
        }

        [HttpPost("close")]
        public async Task<IActionResult> CloseAccount(CloseAccountRequestDto request)
        {
            CloseAccountResponseDto response = await _accountService.CloseAccountAsync(request);
            return Ok(response);
        }
    }
}