using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public string Usuario { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Activo { get; set; }

    public int IdPerfil { get; set; }
}
