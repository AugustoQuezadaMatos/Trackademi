using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using TrackademiApi.Mappers;
using TrackademiApi.Models.Entities;
using TrackademiApi.Repository.Interfaces;
using TrackademiApi.Services.Interfaces;
using TrackademiApi.Utilities;

namespace TrackademiApi.Services
{

    public class MateriasService : GenericService<Materia, MateriaDto>, IMateriasService
    {
        private readonly TrackademiContext _context; // Usa tu contexto real (ej: TrackademiContext)
        private readonly ILogger<MateriasService> _logger;

        public MateriasService(IGenericRepository<Materia> repository, TrackademiContext context, ILogger<MateriasService> logger)
            : base(repository)
        {
            _context = context;
            _logger = logger;
           
        }

        public async Task<IEnumerable<EstudiantesDto>> ObtenerEstudiantesAsociadosConDetalle(int idMateria)
        {
            var idsEstudiantes = await _context.MateriaInscrita
                .Where(mi => mi.IdMateria == idMateria)
                .Select(mi => mi.IdEstudiante)
                .ToListAsync();

            var estudiantes = await _context.Estudiantes
                .Where(e => idsEstudiantes.Contains(e.IdEstudiante))
                .ToListAsync();

            return estudiantes.Select(e => ModelMapper.GetMapper<Estudiantes, EstudiantesDto>().ToViewModel.Map(e));

        }

        public async Task<OperationResult> AsociarEstudiantesAsync(int idMateria, List<int> idsEstudiantes)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var materiaExiste = await _context.Set<Materia>().AnyAsync(m => m.Id == idMateria);
                if (!materiaExiste)
                    return OperationResult.Fail("La materia especificada no existe.");

                // Eliminar asociaciones anteriores
                var existentes = _context.Set<MateriaInscrita>().Where(mi => mi.IdMateria == idMateria);
                _context.RemoveRange(existentes);

                // Crear nuevas asociaciones
                foreach (var idEstudiante in idsEstudiantes)
                {
                    _context.Add(new MateriaInscrita
                    {
                        IdMateria = idMateria,
                        IdEstudiante = idEstudiante
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return OperationResult.Ok("Estudiantes asociados correctamente");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error al asociar estudiantes a la materia");
                return OperationResult.Fail("Error inesperado", new Dictionary<string, string[]>
            {
                { "Exception", new[] { ex.Message } }
            });
            }
        }

        public async Task<List<int>> GetEstudiantesAsociadosAsync(int idMateria)
        {
            return await _context.Set<MateriaInscrita>()
                .Where(mi => mi.IdMateria == idMateria)
                .Select(mi => mi.IdEstudiante)
                .ToListAsync();
        }

        public async Task<IEnumerable<MateriaDto>> BuscarPorNombreAsync(string nombre)
        {
            var Materia = await GetAllAsync();
            return Materia
                .Where(u => !string.IsNullOrWhiteSpace(u.Nombre) && u.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
        }


    }



}



