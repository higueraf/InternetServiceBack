using InternetServiceBack.Bll;
using InternetServiceBack.Models;
using Microsoft.AspNetCore.Mvc;
using InternetServiceBack.Dtos.LoginProcess;


namespace InternetServiceBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginBll _loginBll;

        public LoginController(DatabaseInternetServiceContext dbContext)
        {
            _loginBll = new LoginBll(dbContext);
        }

        // POST api/<LoginController>
        [HttpPost]
        public dynamic Post([FromBody] LoginRequestDto loginRequestDto)
        {
            return _loginBll.GetLoginUSer(loginRequestDto);
        }
    }
}