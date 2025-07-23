using TrackademiApi.Models.Entities;
using TrackademiApi.Utilities;

namespace TrackademiApi.Services.Interfaces
{
    public interface IMateriasService : IGenericService<MateriaDto>
    {
        Task<IEnumerable<MateriaDto>> BuscarPorNombreAsync(string nombre);

        Task<OperationResult> AsociarEstudiantesAsync(int idMateria, List<int> idsEstudiantes);

        Task<List<int>> GetEstudiantesAsociadosAsync(int idMateria);

        Task<IEnumerable<EstudiantesDto>> ObtenerEstudiantesAsociadosConDetalle(int idMateria);
    }

}
