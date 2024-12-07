using CrudWebApiDapper.Dto;
using CrudWebApiDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudWebApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();

            if (usuarios.Status == false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);

        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int idUsuario)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(idUsuario);

            if (usuario.Status == false)
            {
                return NotFound(usuario);
            }

            return Ok(usuario);

        }

        [HttpPost]
        public async Task<IActionResult> InserirUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            var usuarios = await _usuarioInterface.InserirUsuario(usuarioCriarDto);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(usuarioEditarDto);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);

        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirUsuario(int IdUsuario)
        {
            var usuarios = await _usuarioInterface.ExcluirUsuario(IdUsuario);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);

        }
    }
}
