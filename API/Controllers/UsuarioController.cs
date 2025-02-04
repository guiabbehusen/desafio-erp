using DesafioERP.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioERP.API.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Usuario>> BuscarUsuarios()
        {
            return Ok(new List<Usuario>());
        }
    }
}
