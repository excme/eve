using eveDirect.Databases.Contexts.Public.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eveDirect.Services.Api.Identity.Controller
{
    [ApiController]
    [Route("api/auth/")]
    public class AuthenticationController : ControllerBase
    {
        UserManager<Account> _userManager { get; set; }
        SignInManager<Account> _signInManger { get; set; }
        ILogger<AuthenticationController> _logger { get; set; }
        public AuthenticationController(SignInManager<Account> singInManager/*, ILoggerContext loggerContext*/, UserManager<Account> userManager)
        {
            _signInManger = singInManager;
            //_logger = loggerContext;
            _userManager = userManager;
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] SigninModel loginModel) { 

        //}
        //[HttpPost("token")]
        //public async Task<IActionResult> GetToken([FromBody] GetTokenModel getTokenModel) {

        //}
        //private async Task<IActionResult> generateToken()
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByNameAsync(model.UserName);
        //        if(user != null)
        //        {

        //        }
        //    }catch(Exception ex)
        //    {

        //    }
        //}
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout([FromBody] SingoutModel logoutModel) {
        //}
        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        //{
        //}
    }
}
