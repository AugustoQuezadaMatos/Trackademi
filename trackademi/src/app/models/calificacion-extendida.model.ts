export interface CalificacionExtendida {
    idCalificacion: number;
    idMateria: number;
    idEstudiante: number;
    notaParcial: number;
    notaFinal: number;
    observaciones?: string;

    nombres: string;
    apellidos: string;
    matricula: string;
    email: string;
    nombreCompleto?: string;
}
