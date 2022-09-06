using System.Threading.Tasks;
using eveDirect.Databases.PublicCommon.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace eveDirect.Services.Api.Identity.Services
{
    public class EFLoginService : ILoginService<Account>
    {
        private UserManager<Account> _userManager;
        private SignInManager<Account> _signInManager;

        public EFLoginService(UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Account> FindByUsername(string user)
        {
            return await _userManager.FindByEmailAsync(user);
        }

        public async Task<bool> ValidateCredentials(Account user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task SignIn(Account user)
        {
            return _signInManager.SignInAsync(user, true);
        }

        public Task SignInAsync(Account user, AuthenticationProperties properties, string authenticationMethod = null)
        {
            return _signInManager.SignInAsync(user, properties, authenticationMethod);
        }
    }
}
