using DesafioERP.API.Models;
using DesafioERP.API.Services;
using DesafioERP.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly UsuarioService _usuarioService;
        private readonly LoginService _loginService;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IEnderecoRepositorio enderecoRepositorio, UsuarioService usuarioService, LoginService loginService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioService = usuarioService;
            _enderecoRepositorio = enderecoRepositorio;
            _loginService =  loginService;

        }

        [HttpGet("{CPF}")]
        public async Task<ActionResult<Usuario>> BuscaPorCPF(string CPF)
        {
            Usuario usuarios = await _usuarioRepositorio.BuscaPorCPF(CPF);
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Cadastrar([FromBody] Usuario usuario)
        {
            var erros = _usuarioService.ValidarCadastro(usuario);
            if (erros.Count != 0)
            {
                return BadRequest(erros);
            }
            usuario.Senha = _loginService.CriptografarSenha(usuario.Senha);
            Usuario usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario);
            return Ok(usuarioCriado);
        }

        [HttpPut("{CPF}")]
        public async Task<ActionResult<Usuario>> Editar([FromBody] Usuario usuario, string CPF)
        {
            usuario.CPF = CPF;
            Usuario usuario1 = await _usuarioRepositorio.EditarUsuario(usuario, CPF);
            return Ok(usuario1);
        }

        [HttpDelete("{CPF}")]
        public async Task<ActionResult> Apagar(string CPF)
        {
            try
            {
                var usuario = await _usuarioRepositorio.ApagarUsuario(CPF);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("adicionar-endereco/{CPF}")]
        public async Task<ActionResult<Endereco>> AdicionarEndereco(string CPF, [FromBody] Endereco endereco)
        {
            var usuario = await _usuarioRepositorio.BuscaPorCPF(CPF);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            endereco.UsuarioCPF = CPF;

            if (_usuarioService.ValidarEndereco(endereco.Rua, endereco.Numero, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP))
            {

                var enderecoCriado = await _enderecoRepositorio.AdicionarEndereco(endereco);
                return Ok(enderecoCriado);
            }
            return BadRequest("Endereço inválido. Todos os campos devem ser preenchidos e o CEP deve estar no formato correto.");
        }
    }
}
