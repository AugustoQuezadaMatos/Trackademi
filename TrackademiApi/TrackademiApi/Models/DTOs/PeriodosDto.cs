using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrackademiApi.Models.Entities;

public partial class PeriodosDto
{
    public int IdPeriodo { get; set; }
    [Required(ErrorMessage = "El código es obligatorio.")]
    [StringLength(10, ErrorMessage = "El código no debe superar los 10 caracteres.")]
    public string Codigo { get; set; } = null!;

    public int IdUsuario { get; set; }

    public DateTime FechaCreacion { get; set; }
}
