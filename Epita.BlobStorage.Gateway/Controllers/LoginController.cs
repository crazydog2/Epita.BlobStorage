using System.Threading.Tasks;
using Epita.BlobStorage.Gateway.Requests;
using Epita.BlobStorage.Gateway.Security;
using Epita.BlobStorage.Logic.Contracts;
using Epita.BlobStorage.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epita.BlobStorage.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly SecurityManager securityManager;
        private readonly IUserLogic userLogic;

        public LoginController(
            SecurityManager securityManager,
            IUserLogic userLogic)
        {
            this.securityManager = securityManager;
            this.userLogic = userLogic;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await userLogic.LoginAsync(loginRequest.Email, loginRequest.Password).ConfigureAwait(false);

            if (user == null)
            {
                return NotFound();
            }

            await securityManager.SignInAsync(user).ConfigureAwait(false);

            return Ok();
        }
    }
}