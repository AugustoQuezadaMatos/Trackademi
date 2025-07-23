using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using TrackademiApi.Models.Entities;
using TrackademiApi.Utilities;

namespace TrackademiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudiantesService _estudiantesService;

        public EstudiantesController(IEstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lista = await _estudiantesService.GetAllAsync();
                return Ok(OperationResult.Ok("Lista obtenida correctamente", lista));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener la lista", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _estudiantesService.GetByIdAsync(id);
                return item == null
                    ? NotFound(OperationResult.Fail("Estudiante no encontrado"))
                    : Ok(OperationResult.Ok("Estudiante obtenido", item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener estudiante", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EstudiantesDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _estudiantesService.AddAsync(model);
                return Ok(OperationResult.Ok("Estudiante creado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo crear el estudiante", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EstudiantesDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _estudiantesService.UpdateAsync(model);
                return Ok(OperationResult.Ok("Estudiante actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo actualizar el estudiante", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _estudiantesService.DeleteAsync(id);
                return Ok(OperationResult.Ok("Estudiante eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo eliminar el estudiante", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest(OperationResult.Fail("Debes proporcionar un nombre para buscar.", new Dictionary<string, string[]>
        {
            { "nombre", new[] { "El parámetro 'nombre' es requerido." } }
        }));
            }

            try
            {
                var resultado = await _estudiantesService.BuscarPorNombreAsync(nombre);
                return Ok(OperationResult.Ok("Estudiantes filtrados correctamente", resultado));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al buscar estudiantes", new Dictionary<string, string[]>
        {
            { "Exception", new[] { ex.Message } }
        }));
            }
        }


    }
}
