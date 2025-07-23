using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrackademiApi.Models.Entities;

public partial class UsuariosDto
{
    public int IdUsuario { get; set; }
    [Required(ErrorMessage = "Debe digitar un nombre de usuario")]
    [MinLength(4, ErrorMessage = "el usuario debe tener un minimo de 4 caracteres")]
    [MaxLength(20, ErrorMessage = "No puede exceder los 20 caracteres")]
    public string Usuario { get; set; } = null!;
    [Required(ErrorMessage = "Debe digitar una clave")]
    [MinLength(8, ErrorMessage = "la contraseña debe tener un minimo de 8 caracteres")]
    [MaxLength(20, ErrorMessage = "No puede exceder los 20 caracteres")]
    public string Clave { get; set; } = null!;

    public string Correo { get; set; } = null!;
    [Required(ErrorMessage = "Debe digitar un nombre de usuario")]
    [MaxLength(20, ErrorMessage = "No puede exceder los 20 caracteres")]
    public string Nombres { get; set; } = null!;
    [Required(ErrorMessage = "Debe digitar un nombre de usuario")]
    [MaxLength(20, ErrorMessage = "No puede exceder los 20 caracteres")]
    public string Apellidos { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Activo { get; set; }

    public int IdPerfil { get; set; }
}
