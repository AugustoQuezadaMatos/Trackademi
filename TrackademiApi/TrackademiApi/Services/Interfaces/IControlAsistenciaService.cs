using Microsoft.AspNetCore.Mvc;
using TrackademiApi.Models.Entities;

namespace TrackademiApi.Services.Interfaces
{
    public interface IControlAsistenciaService
    {
        Task RegistrarAsistenciasAsync(IEnumerable<ControlAsistenciaDto> asistencias);
        Task<IEnumerable<ControlAsistenciaDto>> ObtenerHistorialPorMateriaAsync(int idMateria);

        Task<IEnumerable<EstudiantesDto>> ObtenerEstudiantesConDetalle(int idMateria);


    }

}
