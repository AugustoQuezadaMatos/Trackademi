using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class ControlAsistencia
{
    public int IdAsistencia { get; set; }

    public int IdMateria { get; set; }

    public int IdEstudiante { get; set; }

    public bool Presente { get; set; }

    public DateTime Fecha { get; set; }
}
