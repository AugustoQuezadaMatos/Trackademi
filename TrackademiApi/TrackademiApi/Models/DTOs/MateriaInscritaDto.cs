using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class MateriaInscritaDto
{
    public int Id { get; set; }

    public int IdEstudiante { get; set; }

    public int IdMateria { get; set; }

    public Estudiantes Estudiante { get; set; }

}
