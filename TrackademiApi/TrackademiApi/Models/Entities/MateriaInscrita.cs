using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class MateriaInscrita
{
    public int Id { get; set; }

    public int IdEstudiante { get; set; }

    public int IdMateria { get; set; }
}
