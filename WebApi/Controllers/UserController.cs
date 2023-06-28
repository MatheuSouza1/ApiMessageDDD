using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.models;
using WebApi.Token;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateIdentityToken([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
            {
                return Unauthorized();
            }

            var result = await _signInManager.PasswordSignInAsync(login.email, login.password, false, false);

            if (result.Succeeded)
            {
                // Recupera Usuário Logado
                var currentUser = await _userManager.FindByEmailAsync(login.email);
                var userId = currentUser.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.CreateToken("Secret_Key-12345678"))
                .AddSubject("Matheus")
                .AddIssuer("Testing.Securiry.Bearer")
                .AddAudience("Testing.Securiry.Bearer")
                .AddClaim("userId", userId)
                .AddExpiry(5)
                .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
