using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class LogDto
{
    public int IdLog { get; set; }

    public int IdUsuario { get; set; }

    public string Metodo { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Data { get; set; } = null!;

    public DateTime Fecha { get; set; }
}
