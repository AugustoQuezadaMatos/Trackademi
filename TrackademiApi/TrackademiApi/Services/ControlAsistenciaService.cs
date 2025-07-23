using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrackademiApi.Mappers;
using TrackademiApi.Models.Entities;
using TrackademiApi.Repository.Interfaces;
using TrackademiApi.Services.Interfaces;

namespace TrackademiApi.Services
{
    public class ControlAsistenciaService : GenericService<ControlAsistencia, ControlAsistenciaDto>, IControlAsistenciaService
    {
        private readonly TrackademiContext _context;

        public ControlAsistenciaService(
     IGenericRepository<ControlAsistencia> repository,
     TrackademiContext context
 ) : base(repository) // ✅ Esto satisface el constructor base
        {
            _context = context;
        }

        public async Task RegistrarAsistenciasAsync(IEnumerable<ControlAsistenciaDto> asistencias)
        {
            foreach (var dto in asistencias)
            {
                var existe = await _context.ControlAsistencia.AnyAsync(a =>
                    a.IdEstudiante == dto.IdEstudiante &&
                    a.IdMateria == dto.IdMateria &&
                    a.Fecha.Date == dto.Fecha.Date);

                if (!existe)
                {
                    var nueva = new ControlAsistencia
                    {
                        IdEstudiante = dto.IdEstudiante,
                        IdMateria = dto.IdMateria,
                        Fecha = dto.Fecha.Date,
                        Presente = dto.Presente
                    };

                    _context.ControlAsistencia.Add(nueva);
                }
            }

            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<EstudiantesDto>> ObtenerEstudiantesConDetalle(int idMateria)
        {
            var idsEstudiantes = await _context.MateriaInscrita.Where(mi => mi.IdMateria == idMateria)
                .Select(mi => mi.IdEstudiante)
                .ToListAsync();

            var estudiantes = await _context.Estudiantes
                .Where(e => idsEstudiantes.Contains(e.IdEstudiante))
                .ToListAsync();

            return estudiantes
                .Select(e => ModelMapper
                    .GetMapper<Estudiantes, EstudiantesDto>()
                    .ToViewModel
                    .Map(e));
        }

        public async Task<IEnumerable<ControlAsistenciaDto>> ObtenerHistorialPorMateriaAsync(int idMateria)
        {
            var historial = await (
                from asistencia in _context.ControlAsistencia
                join estudiante in _context.Estudiantes
                    on asistencia.IdEstudiante equals estudiante.IdEstudiante into estudianteJoin
                from estudiante in estudianteJoin.DefaultIfEmpty()  // LEFT JOIN con estudiantes

                join materia in _context.Materia
                    on asistencia.IdMateria equals materia.Id into materiaJoin
                from materia in materiaJoin.DefaultIfEmpty()        // LEFT JOIN con materia

                where asistencia.IdMateria == idMateria

                orderby asistencia.Fecha descending

                select new ControlAsistenciaDto
                {
                    IdEstudiante = asistencia.IdEstudiante,
                    Nombres = estudiante != null ? estudiante.Nombres : "",
                    Apellidos = estudiante != null ? estudiante.Apellidos : "",
                    Matricula = estudiante != null ? estudiante.Matricula : "",
                    NombresMateria = materia != null ? materia.Nombre : "",
                    Fecha = asistencia.Fecha,
                    Presente = asistencia.Presente
                }
            ).ToListAsync();

            return historial;
        }


    }

}
