using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
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
        [HttpPost("/api/CreateIdentityToken")]
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

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddIdentityToken")]
        public async Task<IActionResult> AdicionaUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Ok("Falta alguns dados");


            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                Cpf = login.cpf,
                UserType = UserType.Basic,
            };

            var resultado = await _userManager.CreateAsync(user, login.password);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // return email
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao confirmar usuários");

        }
    }
}
