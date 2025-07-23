using Microsoft.AspNetCore.Mvc;
using TrackademiApi.Models.DTOs;
using TrackademiApi.Models.Entities;
using TrackademiApi.Services.Interfaces;
using TrackademiApi.Utilities;

namespace TrackademiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodosController : ControllerBase
    {
        private readonly IGenericService<PeriodosDto> _service;

        public PeriodosController(IGenericService<PeriodosDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _service.GetAllAsync();
                return Ok(OperationResult.Ok("Lista de períodos", lista));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener los períodos", new Dictionary<string, string[]>
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                return item == null
                    ? NotFound(OperationResult.Fail("Período no encontrado"))
                    : Ok(OperationResult.Ok("Período encontrado", item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("Error al obtener período", new Dictionary<string, string[]>
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PeriodosDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                model.FechaCreacion = DateTime.Now;
                await _service.AddAsync(model);
                return Ok(OperationResult.Ok("Período creado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo crear el período", new Dictionary<string, string[]>
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PeriodosDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            try
            {
                await _service.UpdateAsync(model);
                return Ok(OperationResult.Ok("Período actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo actualizar el período", new Dictionary<string, string[]>
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
                await _service.DeleteAsync(id);
                return Ok(OperationResult.Ok("Período eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult.Fail("No se pudo eliminar el período", new Dictionary<string, string[]>
                {
                    { "Exception", new[] { ex.Message } }
                }));
            }
        }
    }
}
