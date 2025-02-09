// rota para realizar o login
// essa rota deve receber um body (json) no corpo da requisicao: usuario e senha
// essa rota que realizara a autenticacao e devera retornar um token (JWT)
// para conferir se o usuario esta autenticado, tem que buscar o usuario no BD pelo usuario 
// se existir, conferir se a senha retornada do bd (senha criptografada) bate com a senha passada 
// entao a senha passada tera que passar por um processo de criptografia antes de fazer a conferencia


// 1234546
// usuario: senha: dhaskjuhduiqwhdaiusndauishdieywqiubndaikusjbciuasy42189731298
// 12334456 tem que fazer o hash dela e com o hash comparar com dhaskjuhduiqwhdaiusndauishdieywqiubndaikusjbciuasy42189731298

// cadatro do usuario 
// receber a senha 1234546 e fazer o hash


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
        public async Task<ActionResult<string>> Login([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Login) || string.IsNullOrEmpty(usuario.Senha))
            {
                return BadRequest("Usuário e senha são obrigatórios.");
            }

            bool loginValido = await _loginService.ValidarLogin(usuario.Login, usuario.Senha);

            if (!loginValido)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            return Ok("Login realizado com sucesso.");
        }

    }
}
