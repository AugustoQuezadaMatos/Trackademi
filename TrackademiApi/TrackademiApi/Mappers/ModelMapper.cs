using System.Collections.Concurrent;
using TrackademiApi.Mappers;
using TrackademiApi.Models.Entities;
using System;

namespace TrackademiApi.Mappers
{
    public static class ModelMapper
    {
        // Diccionario global: (Entidad, ViewModel) → MapperPair
        private static readonly ConcurrentDictionary<(Type Entity, Type Model), object> _mappers = new();

        // Registrar todos los mapeos aquí
        static ModelMapper()
        {
            Register<Estudiantes, EstudiantesDto>(
                e => new EstudiantesDto
                {
                    IdEstudiante = e.IdEstudiante,
                    Nombres = e.Nombres,
                    Matricula = e.Matricula,
                    FechaNacimiento = e.FechaNacimiento,
                    Apellidos = e.Apellidos,
                    Email = e.Email,
                    FechaCreacion = e.FechaCreacion,
                    Genero = e.Genero,
                    IdUsuario = e.IdUsuario,
                    Telefono = e.Telefono
                },
                vm => new Estudiantes
                {
                    IdEstudiante = vm.IdEstudiante,
                    Nombres = vm.Nombres,
                    Matricula = vm.Matricula,
                    FechaNacimiento = vm.FechaNacimiento,
                    Apellidos = vm.Apellidos,
                    Telefono = vm.Telefono,
                    IdUsuario = vm.IdUsuario,
                    Genero = vm.Genero,
                    FechaCreacion = vm.FechaCreacion,
                    Email = vm.Email

                }
            );

            Register<Usuarios, UsuariosDto>(
                 e => new UsuariosDto
                 {
                     Usuario = e.Usuario,
                     Nombres = e.Nombres,
                     Activo = e.Activo,
                     Apellidos = e.Apellidos,
                     Clave = e.Clave,
                     Correo = e.Correo,
                     FechaCreacion = e.FechaCreacion,
                     IdPerfil = e.IdPerfil,
                     IdUsuario = e.IdUsuario
                 },
                 vm => new Usuarios
                 {
                     Usuario = vm.Usuario,
                     Nombres = vm.Nombres,
                     IdPerfil = vm.IdPerfil,
                     Activo = vm.Activo,
                     Apellidos = vm.Apellidos,
                     Clave = vm.Clave,
                     Correo = vm.Correo,
                     IdUsuario = vm.IdUsuario,
                     FechaCreacion = vm.FechaCreacion
                 }
             );

            Register<Calificaciones, CalificacionesDto>(
               e => new CalificacionesDto
               {
                   IdCalificacion = e.IdCalificacion,
                   IdEstudiante = e.IdEstudiante,
                   IdMateria = e.IdMateria,
                   NotaFinal = e.NotaFinal,
                   NotaParcial = e.NotaParcial,
                   Observaciones = e.Observaciones
               },
               vm => new Calificaciones
               {
                   Observaciones = vm.Observaciones,
                   NotaFinal = vm.NotaFinal,
                   IdCalificacion = vm.IdCalificacion,
                   NotaParcial = vm.NotaParcial,
                   IdEstudiante = vm.IdEstudiante,
                   IdMateria = vm.IdMateria
               }
           );
            Register<Perfiles, PerfilesDto>(
               e => new PerfilesDto
               {
                   Descripcion = e.Descripcion,
                   IdPerfil = e.IdPerfil,
                   Nombre = e.Nombre,
               },
               vm => new Perfiles
               {
                   IdPerfil = vm.IdPerfil,
                   Nombre = vm.Nombre,
                   Descripcion = vm.Descripcion,
               }
           );

            Register<Materia, MateriaDto>(
               e => new MateriaDto
               {
                   Detalle = e.Detalle,
                   Id = e.Id,
                   IdPeriodo = e.IdPeriodo,
                   Nombre = e.Nombre,

               },
               vm => new Materia
               {
                   Detalle = vm.Detalle,
                   Id = vm.Id,
                   IdPeriodo = vm.IdPeriodo,
                   Nombre = vm.Nombre,
               }
           );

            Register<Perfiles, PerfilesDto>(e => new PerfilesDto
            {
                Descripcion = e.Descripcion,
                IdPerfil = e.IdPerfil,
                Nombre = e.Nombre,
            },
            vm => new Perfiles
            {
                Descripcion = vm.Descripcion,
                IdPerfil = vm.IdPerfil,
                Nombre = vm.Nombre

            });
            Register<Periodos, PeriodosDto>(e => new PeriodosDto
            {
                Codigo = e.Codigo,
                FechaCreacion = e.FechaCreacion,
                IdPeriodo = e.IdPeriodo,
                IdUsuario = e.IdUsuario
            },
             vm => new Periodos
             {
                 Codigo = vm.Codigo,
                 FechaCreacion = vm.FechaCreacion,
                 IdPeriodo = vm.IdPeriodo,
                 IdUsuario = vm.IdUsuario

             });

            Register<ControlAsistencia, ControlAsistenciaDto>(e => new ControlAsistenciaDto{
            Fecha =e.Fecha,
            IdAsistencia = e.IdAsistencia,
            IdEstudiante = e.IdEstudiante,
            IdMateria = e.IdMateria,
            Presente =e.Presente
            },vm => new ControlAsistencia { 
                Fecha = vm.Fecha,
                Presente=vm.Presente,
                IdAsistencia=vm.IdAsistencia,
                IdMateria=vm.IdMateria,
                IdEstudiante =vm.IdEstudiante,
            
            });
        }

        // Contenedor del par de mapeadores (a entidad y a modelo)
        public class MapperPair<TEntity, TModel>
        {
            public ObjectsMapper<TEntity, TModel> ToViewModel { get; set; } = default!;
            public ObjectsMapper<TModel, TEntity> ToEntity { get; set; } = default!;
        }

        // Registrar mapeo manualmente
        public static void Register<TEntity, TModel>(
            Func<TEntity, TModel> toModel,
            Func<TModel, TEntity> toEntity
        )
        {
            var key = (typeof(TEntity), typeof(TModel));
            var value = new MapperPair<TEntity, TModel>
            {
                ToViewModel = new ObjectsMapper<TEntity, TModel>(toModel),
                ToEntity = new ObjectsMapper<TModel, TEntity>(toEntity)
            };
            _mappers[key] = value;
        }

        // Obtener el mapeador
        public static MapperPair<TEntity, TModel> GetMapper<TEntity, TModel>()
        {
            var key = (typeof(TEntity), typeof(TModel));
            if (_mappers.TryGetValue(key, out var value) && value is MapperPair<TEntity, TModel> mapper)
                return mapper;

            throw new InvalidOperationException($"No mapper registered for {typeof(TEntity).Name} <-> {typeof(TModel).Name}");
        }
    }
}
