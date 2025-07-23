using TrackademiApi.Models.DTOs;
using TrackademiApi.Models.Entities;
using TrackademiApi.Services.Interfaces;

namespace Services.Interfaces
{
    public interface ICalificacionesService : IGenericService<CalificacionesDto>
    {
        string ObtenerLiteral(int notaFinal);
        Task<IEnumerable<CalificacionesDto>> ObtenerPorEstudianteAsync(int idEstudiante);
        Task<IEnumerable<CalificacionesDto>> ObtenerPorMateriaAsync(int idMateria);

        Task<IEnumerable<CalificacionExtendidaDto>> ObtenerConEstudiantesPorMateria(int idMateria);

    }
}
