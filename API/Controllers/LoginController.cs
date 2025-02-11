using DesafioERP.API.Models;
using DesafioERP.API.Services;
using DesafioERP.Repositorios.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DesafioERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginResource loginResource)
        {
            if (loginResource == null || string.IsNullOrEmpty(loginResource.Login) || string.IsNullOrEmpty(loginResource.Senha))
            {
                return BadRequest("Usuário e senha são obrigatórios.");
            }

            Console.WriteLine(loginResource.Login);
            Console.WriteLine(loginResource.Senha);


            bool loginValido = await _loginService.ValidarLogin(loginResource.Login, loginResource.Senha);

            if (!loginValido)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            string cpfUsuario = await _loginService.ObterCPFPorLogin(loginResource.Login);

            return Ok(new { CPF = cpfUsuario });
        }


    }
}
