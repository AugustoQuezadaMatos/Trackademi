using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using TrackademiApi.Models.Entities;
using TrackademiApi.Utilities;

namespace TrackademiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lista = await _usuariosService.GetAllAsync();
                return Ok(OperationResult.Ok("Lista de usuarios", lista));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener usuarios", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _usuariosService.GetByIdAsync(id);
                return item == null
                    ? NotFound(OperationResult.Fail("Usuario no encontrado"))
                    : Ok(OperationResult.Ok("Usuario encontrado", item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener usuario", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuariosDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _usuariosService.AddAsync(model);
                return Ok(OperationResult.Ok("Usuario creado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo crear el usuario", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UsuariosDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _usuariosService.UpdateAsync(model);
                return Ok(OperationResult.Ok("Usuario actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo actualizar el usuario", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuariosService.DeleteAsync(id);
                return Ok(OperationResult.Ok("Usuario eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo eliminar el usuario", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorCorreo([FromQuery] string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return BadRequest(OperationResult.Fail("Debes proporcionar un correo para buscar.", new Dictionary<string, string[]>
                {
                    { "correo", new[] { "El parámetro 'correo' es requerido." } }
                }));
            }

            try
            {
                var resultado = await _usuariosService.BuscarPorCorreoAsync(correo);
                return Ok(OperationResult.Ok("Usuarios filtrados correctamente", resultado));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al buscar usuarios", new Dictionary<string, string[]>
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }
    }
}
