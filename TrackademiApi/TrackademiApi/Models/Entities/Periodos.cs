using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class Periodos
{
    public int IdPeriodo { get; set; }

    public string Codigo { get; set; } = null!;

    public int IdUsuario { get; set; }

    public DateTime FechaCreacion { get; set; }
}
