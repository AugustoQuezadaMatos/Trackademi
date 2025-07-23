using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using TrackademiApi.Models.DTOs;
using TrackademiApi.Models.Entities;
using TrackademiApi.Repository.Interfaces;

namespace TrackademiApi.Services
{
    public class CalificacionesService : GenericService<Calificaciones, CalificacionesDto>, ICalificacionesService
    {
        private readonly TrackademiContext _context;

        public CalificacionesService(IGenericRepository<Calificaciones> repository, TrackademiContext context)
            : base(repository)
        {
            _context = context;
        }


        public string ObtenerLiteral(int notaFinal)
        {
            return notaFinal switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                _ => "F"
            };
        }

        public async Task<IEnumerable<CalificacionesDto>> ObtenerPorEstudianteAsync(int idEstudiante)
        {
            var calificaciones = await _context.Calificaciones
                .Where(c => c.IdEstudiante == idEstudiante)
                .ToListAsync();

            return calificaciones.Select(c => _mapper.ToViewModel.Map(c));
        }

        public async Task<IEnumerable<CalificacionesDto>> ObtenerPorMateriaAsync(int idMateria)
        {
            var calificaciones = await _context.Calificaciones
                .Where(c => c.IdMateria == idMateria)
                .ToListAsync();

            return calificaciones.Select(c => _mapper.ToViewModel.Map(c));
        }


        public async Task<IEnumerable<CalificacionExtendidaDto>> ObtenerConEstudiantesPorMateria(int idMateria)
        {
            var resultado = await (
                from mi in _context.MateriaInscrita
                join est in _context.Estudiantes
                    on mi.IdEstudiante equals est.IdEstudiante
                join cal in _context.Calificaciones
                    on new { mi.IdEstudiante, mi.IdMateria } equals new { cal.IdEstudiante, cal.IdMateria } into calGroup
                from cal in calGroup.DefaultIfEmpty() // ← LEFT JOIN
                where mi.IdMateria == idMateria
                select new CalificacionExtendidaDto
                {
                    IdEstudiante = est.IdEstudiante,
                    Nombres = est.Nombres,
                    Apellidos = est.Apellidos,
                    Matricula = est.Matricula,
                    Email = est.Email,

                    IdCalificacion = cal != null ? cal.IdCalificacion : null,
                    IdMateria = mi.IdMateria,
                    NotaParcial = cal.NotaParcial,
                    NotaFinal = cal.NotaFinal,
                    Observaciones = cal.Observaciones
                }
            ).ToListAsync();

            return resultado;
        }


    }
}
