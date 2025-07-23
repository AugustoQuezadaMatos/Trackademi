export interface Calificacion {
    idCalificacion: number;
    idMateria: number;
    idEstudiante: number;
    notaParcial: number;
    notaFinal?: number;
    observaciones?: string;
}
