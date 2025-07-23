using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using TrackademiApi.Models.Entities;
using TrackademiApi.Services.Interfaces;
using TrackademiApi.Utilities;

namespace TrackademiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriasService _materiasService;

        public MateriasController(IMateriasService materiasService)
        {
            _materiasService = materiasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lista = await _materiasService.GetAllAsync();
              
                return Ok(OperationResult.Ok("Materias obtenidas", lista));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener materias", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _materiasService.GetByIdAsync(id);
                return item == null
                    ? NotFound(OperationResult.Fail("Materia no encontrada"))
                    : Ok(OperationResult.Ok("Materia encontrada", item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener materia", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MateriaDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _materiasService.AddAsync(model);
                return Ok(OperationResult.Ok("Materia creada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo crear la materia", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MateriaDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _materiasService.UpdateAsync(model);
                return Ok(OperationResult.Ok("Materia actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo actualizar la materia", new Dictionary<string, string[]> {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _materiasService.DeleteAsync(id);
                return Ok(OperationResult.Ok("Materia eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo eliminar la materia", new Dictionary<string, string[]> {
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
                var resultado = await _materiasService.BuscarPorNombreAsync(nombre);
                return Ok(OperationResult.Ok("Materias filtradas correctamente", resultado));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al buscar materias", new Dictionary<string, string[]>
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }


        [HttpPut("asociar-estudiantes/{idMateria}")]
        public async Task<IActionResult> AsociarEstudiantes(int idMateria, [FromBody] List<int> idsEstudiantes)
        {
            var resultado = await _materiasService.AsociarEstudiantesAsync(idMateria, idsEstudiantes);

            if (!resultado.Success)
            {
                if (resultado.Message == "La materia especificada no existe.")
                    return NotFound(resultado);

                return StatusCode(500, resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("estudiantes-asociados/{idMateria}")]
        public async Task<IActionResult> GetEstudiantesAsociados(int idMateria)
        {
            try
            {
                var ids = await _materiasService.GetEstudiantesAsociadosAsync(idMateria);
                return Ok(OperationResult.Ok("Estudiantes asociados obtenidos", ids));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener los estudiantes asociados", new Dictionary<string, string[]> {
            { "Exception", new[] { ex.Message } }
        }));
            }
        }

        [HttpGet("cantidad-estudiantes/{idMateria}")]
        public async Task<IActionResult> GetCantidadEstudiantesAsociados(int idMateria)
        {
            try
            {
                var cantidad = await _materiasService.GetEstudiantesAsociadosAsync(idMateria);
                
                return Ok(OperationResult.Ok("Cantidad de estudiantes asociados", cantidad.Count()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener la cantidad", new Dictionary<string, string[]>
        {
            { "Exception", new[] { ex.Message } }
        }));
            }
        }

        [HttpGet("estudiantes-detalle/{idMateria}")]
        public async Task<IActionResult> ObtenerEstudiantesAsociadosConDetalle(int idMateria)
        {
            try
            {
                var estudiantes = await _materiasService.ObtenerEstudiantesAsociadosConDetalle(idMateria);
                return Ok(OperationResult.Ok("Estudiantes obtenidos", estudiantes));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener estudiantes", new Dictionary<string, string[]>
        {
            { "Exception", new[] { ex.Message } }
        }));
            }
        }




    }
}
