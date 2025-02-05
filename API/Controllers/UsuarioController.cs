using DesafioERP.API.Models;
using DesafioERP.Repositorios;
using DesafioERP.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
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
            Usuario usuario1 = await _usuarioRepositorio.CriarUsuario(usuario);
            return Ok(usuario1);
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
    }
}

