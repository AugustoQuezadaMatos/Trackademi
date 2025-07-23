using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class Calificaciones
{
    public int IdCalificacion { get; set; }

    public int IdMateria { get; set; }

    public int IdEstudiante { get; set; }

    public int NotaParcial { get; set; }

    public string? Observaciones { get; set; }

    public int? NotaFinal { get; set; }
}
