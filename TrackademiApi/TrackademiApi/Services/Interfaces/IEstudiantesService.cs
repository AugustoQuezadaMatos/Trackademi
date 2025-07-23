using TrackademiApi.Models.Entities;
using TrackademiApi.Services.Interfaces;


namespace Services.Interfaces
{
     public interface IEstudiantesService : IGenericService<EstudiantesDto>
    {
        Task<IEnumerable<EstudiantesDto>> BuscarPorNombreAsync(string nombre);

    }
}
