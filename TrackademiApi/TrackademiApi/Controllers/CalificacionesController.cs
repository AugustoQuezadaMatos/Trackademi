using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using TrackademiApi.Models.Entities;
using TrackademiApi.Utilities;

namespace TrackademiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalificacionesController : ControllerBase
    {
        private readonly ICalificacionesService _calificacionesService;

        public CalificacionesController(ICalificacionesService calificacionesService)
        {
            _calificacionesService = calificacionesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lista = await _calificacionesService.GetAllAsync();
                return Ok(OperationResult.Ok("Lista de calificaciones obtenida correctamente", lista));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener las calificaciones", new()
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _calificacionesService.GetByIdAsync(id);
                return item == null
                    ? NotFound(OperationResult.Fail("Calificación no encontrada"))
                    : Ok(OperationResult.Ok("Calificación obtenida correctamente", item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener la calificación", new()
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CalificacionesDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _calificacionesService.AddAsync(model);
                return Ok(OperationResult.Ok("Calificación creada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al crear la calificación", new()
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CalificacionesDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _calificacionesService.UpdateAsync(model);
                return Ok(OperationResult.Ok("Calificación actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al actualizar la calificación", new()
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _calificacionesService.DeleteAsync(id);
                return Ok(OperationResult.Ok("Calificación eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al eliminar la calificación", new()
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("estudiante/{idEstudiante}")]
        public async Task<IActionResult> GetByEstudiante(int idEstudiante)
        {
            try
            {
                var resultado = await _calificacionesService.ObtenerPorEstudianteAsync(idEstudiante);
                return Ok(OperationResult.Ok("Calificaciones por estudiante obtenidas correctamente", resultado));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener calificaciones del estudiante", new()
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("materia/{idMateria}")]
        public async Task<IActionResult> GetByMateria(int idMateria)
        {
            try
            {
                var resultado = await _calificacionesService.ObtenerPorMateriaAsync(idMateria);
                return Ok(OperationResult.Ok("Calificaciones por materia obtenidas correctamente", resultado));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener calificaciones por materia", new()
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("literal/{notaFinal}")]
        public IActionResult ObtenerLiteral(int notaFinal)
        {
            try
            {
                var literal = _calificacionesService.ObtenerLiteral(notaFinal);
                return Ok(OperationResult.Ok("Literal calculado correctamente", new { notaFinal, literal }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener literal", new()
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }


        [HttpPost("lote")]
        public async Task<IActionResult> GuardarLote([FromBody] List<CalificacionesDto> calificaciones)
        {
            if (calificaciones == null || !calificaciones.Any())
            {
                return BadRequest(OperationResult.Fail("No se recibieron calificaciones"));
            }

            try
            {
                foreach (var item in calificaciones)
                {
                    if (item.IdCalificacion == 0)
                    {
                        await _calificacionesService.AddAsync(item);
                    }
                    else
                    {
                        await _calificacionesService.UpdateAsync(item);
                    }
                }

                return Ok(OperationResult.Ok("Calificaciones guardadas correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al guardar calificaciones", new Dictionary<string, string[]>
        {
            { "Exception", new[] { ex.Message } }
        }));
            }
        }

        [HttpGet("completo/materia/{idMateria}")]
        public async Task<IActionResult> GetExtendidasPorMateria(int idMateria)
        {
            var resultado = await _calificacionesService.ObtenerConEstudiantesPorMateria(idMateria);
            return Ok(OperationResult.Ok("Calificaciones obtenidas correctamente", resultado));

        }

    }
}
