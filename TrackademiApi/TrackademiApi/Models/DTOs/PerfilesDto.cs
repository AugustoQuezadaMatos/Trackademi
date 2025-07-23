using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrackademiApi.Models.Entities;

public partial class PerfilesDto
{
    public int IdPerfil { get; set; }
    [Required(ErrorMessage = "Debe digitar un nombre para el perfil")]
    [MinLength(5,ErrorMessage = "El nombre del perfil debe tener minimo 5 letras")]
    [MaxLength(20,ErrorMessage ="No puede exceder los 20 caracteres")]
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }


}
