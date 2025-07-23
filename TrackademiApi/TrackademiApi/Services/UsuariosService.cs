using Services.Interfaces;
using TrackademiApi.Models.Entities;
using TrackademiApi.Repository.Interfaces;
using TrackademiApi.Services;

namespace TrackademiApi.Services
{
       public class UsuariosService : GenericService<Usuarios, UsuariosDto>, IUsuariosService
    {
        public UsuariosService(IGenericRepository<Usuarios> repository)
            : base(repository)
        {
        }

        public async Task<IEnumerable<UsuariosDto>> BuscarPorCorreoAsync(string correo)
        {
            var usuarios = await GetAllAsync();
            return usuarios
                .Where(u => !string.IsNullOrWhiteSpace(u.Correo) && u.Correo.Contains(correo, StringComparison.OrdinalIgnoreCase));
        }
    }
}
