using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class EstudiantesDto
{
    public int IdEstudiante { get; set; }

    public int IdUsuario { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Matricula { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Genero { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public DateTime? FechaCreacion { get; set; }
}
