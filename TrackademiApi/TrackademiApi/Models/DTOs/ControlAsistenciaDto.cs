using System;
using System.Collections.Generic;

namespace TrackademiApi.Models.Entities;

public partial class ControlAsistenciaDto
{
    public int IdAsistencia { get; set; }

    public int IdMateria { get; set; }

    public int IdEstudiante { get; set; }

    public bool Presente { get; set; }

    public DateTime Fecha { get; set; }


    public string? Nombres { get; set; }
    public string? NombresMateria { get; set; }
    public string? Matricula{ get; set; }
    public string? Apellidos { get; set; }

                    
                     
}
