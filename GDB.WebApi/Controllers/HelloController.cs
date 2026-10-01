using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

 
namespace GDB.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from Global Digital Bank API");
        }
    }
}