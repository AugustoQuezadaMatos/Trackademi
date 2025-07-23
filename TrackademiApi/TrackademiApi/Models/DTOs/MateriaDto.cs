using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class MateriaDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdPeriodo { get; set; }

    public string Detalle { get; set; } = null!;
  
}
