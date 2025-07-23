namespace TrackademiApi.Models.DTOs
{
    public class CalificacionExtendidaDto
    {
        public int IdEstudiante { get; set; }
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Matricula { get; set; } = "";
        public string Email { get; set; } = "";

        public int? IdCalificacion { get; set; }
        public int IdMateria { get; set; }

        public int? NotaParcial { get; set; }
        public int? NotaFinal { get; set; }
        public string? Observaciones { get; set; }
    }

}
