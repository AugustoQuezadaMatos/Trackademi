using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using TrackademiApi.Models.Entities;
using TrackademiApi.Services.Interfaces.Services.Interfaces;
using TrackademiApi.Utilities;

namespace TrackademiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilesController : ControllerBase
    {
        private readonly IPerfilesService _service;

        public PerfilesController(IPerfilesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var perfiles = await _service.GetAllAsync();
            return Ok(OperationResult.Ok("Lista de perfiles", perfiles));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PerfilesDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.FormatearErroresModelo(ModelState));

            await _service.AddAsync(model);
            return Ok(OperationResult.Ok("Perfil creado correctamente", model));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PerfilesDto model)
        {
            await _service.UpdateAsync(model);
            return Ok(OperationResult.Ok("Perfil actualizado correctamente", model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(OperationResult.Ok("Perfil eliminado correctamente"));
        }
    }
}
