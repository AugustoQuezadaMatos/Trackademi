using TrackademiApi.Models.Entities;
using TrackademiApi.Repository.Interfaces;
using TrackademiApi.Services.Interfaces.Services.Interfaces;
using TrackademiApi.Models.Entities;

namespace TrackademiApi.Services
{
    public class PerfilesService : GenericService<Perfiles, PerfilesDto>, IPerfilesService
    {
        public PerfilesService(IGenericRepository<Perfiles> repository)
            : base(repository)
        {
        }
    }
}
