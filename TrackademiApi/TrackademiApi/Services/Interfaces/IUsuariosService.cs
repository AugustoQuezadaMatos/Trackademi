using TrackademiApi.Models;
using TrackademiApi.Models.Entities;
using TrackademiApi.Services.Interfaces;

namespace Services.Interfaces
{
    public interface IUsuariosService : IGenericService<UsuariosDto>
    {
        Task<IEnumerable<UsuariosDto>> BuscarPorCorreoAsync(string correo);
    }
}
