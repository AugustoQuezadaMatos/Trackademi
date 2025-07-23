using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackademiApi.Models.Entities;
using TrackademiApi.Services;
using TrackademiApi.Services.Interfaces;
using TrackademiApi.Utilities;

namespace TrackademiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControlAsistenciaController : ControllerBase
    {
        private readonly IControlAsistenciaService _service;

        public ControlAsistenciaController(IControlAsistenciaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] List<ControlAsistenciaDto> registros)
        {
            if (registros == null || !registros.Any())
                return BadRequest(OperationResult.Fail("No se enviaron registros."));

            try
            {
                await _service.RegistrarAsistenciasAsync(registros);
                return Ok(OperationResult.Ok("Asistencia registrada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al registrar asistencia", new Dictionary<string, string[]> {
                { "Exception", new[] { ex.Message } }
            }));
            }
        }

        //[HttpGet("historial/{idMateria}")]
        //public async Task<IActionResult> HistorialPorMateria(int idMateria)
        //{
        //    try
        //    {
        //        var resultado = await _service.ObtenerHistorialPorMateriaAsync(idMateria);
        //        return Ok(OperationResult.Ok("Historial cargado", resultado));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, OperationResult.Fail("Error al cargar historial", new Dictionary<string, string[]>
        //        {
        //            { "Exception", new[] { ex.Message } }
        //        }));
        //    }
        //}


        [HttpGet("estudiantes/{idMateria}")]
        public async Task<IActionResult> ObtenerEstudiantesConDetalle(int idMateria)
        {
            try
            {
                var resultado = await _service.ObtenerEstudiantesConDetalle(idMateria);
                return Ok(OperationResult.Ok("Estudiantes obtenidos", resultado));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener estudiantes", new Dictionary<string, string[]>
        {
            { "Exception", new[] { ex.Message } }
        }));
            }
        }

        [HttpGet("historial/{idMateria}")]
        public async Task<IActionResult> ObtenerHistorialPorMateria(int idMateria)
        {
            var historial = await _service.ObtenerHistorialPorMateriaAsync(idMateria);
            return Ok(OperationResult.Ok("Historial cargado correctamente", historial));

        }




    }

}
