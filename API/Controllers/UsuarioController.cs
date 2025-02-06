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

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IEnderecoRepositorio enderecoRepositorio, UsuarioService usuarioService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioService = usuarioService;
            _enderecoRepositorio = enderecoRepositorio;
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
            if (!_usuarioService.ValidarCPF(usuario.CPF))
                return BadRequest("CPF inválido.");

            if (!_usuarioService.ValidarNome(usuario.Nome))
                return BadRequest("O nome deve conter pelo menos 4 caracteres e conter apenas letras e espaços. ");

            if (!_usuarioService.ValidarEmail(usuario.Email))
                return BadRequest("E-mail inválido.");

            if (!_usuarioService.ValidarTelefone(usuario.Telefone))
                return BadRequest("Telefone inválido.");

            if (!_usuarioService.ValidarSenha(usuario.Senha))
                return BadRequest("Senha inválida. A senha deve conter letras maiúsculas, minúsculas, números e caracteres especiais.");

            foreach (var endereco in usuario.Enderecos)
            {
                if (!_usuarioService.ValidarEndereco(endereco.Rua, endereco.Numero, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP))
                {
                    return BadRequest("Endereço inválido. Todos os campos devem ser preenchidos e o CEP deve estar no formato correto.");
                }
            }

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


            if (!_usuarioService.ValidarEndereco(endereco.Rua, endereco.Numero, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP))
                return BadRequest("Endereço inválido. Todos os campos devem ser preenchidos e o CEP deve estar no formato correto.");

            var enderecoCriado = await _enderecoRepositorio.AdicionarEndereco(endereco);
            return Ok(enderecoCriado);
        }
    }
}
