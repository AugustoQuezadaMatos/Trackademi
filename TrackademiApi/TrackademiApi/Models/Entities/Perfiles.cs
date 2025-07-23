using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class Perfiles
{
    public int IdPerfil { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
