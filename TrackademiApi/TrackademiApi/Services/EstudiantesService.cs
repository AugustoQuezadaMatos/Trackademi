using Services.Interfaces;
using TrackademiApi.Models.Entities;
using TrackademiApi.Repository.Interfaces;
using TrackademiApi.Services;
using Services;
namespace TrackademiApi.Services
{
    public class EstudiantesService : GenericService<Estudiantes, EstudiantesDto>, IEstudiantesService
    {
        public EstudiantesService(IGenericRepository<Estudiantes> repository)
            : base(repository)
        {
        }

        public async Task<IEnumerable<EstudiantesDto>> BuscarPorNombreAsync(string nombre)
        {
            var estudiantes = await GetAllAsync();
            var filtrados = estudiantes
                .Where(e => !string.IsNullOrWhiteSpace(e.Nombres) && e.Nombres.Contains(nombre, StringComparison.OrdinalIgnoreCase));
            return filtrados;
        }


    }
}
