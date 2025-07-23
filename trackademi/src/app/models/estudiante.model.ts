export interface Estudiante {
    idEstudiante: number;
    idUsuario: number;
    nombres: string;
    apellidos: string;
    matricula: string;
    email: string;
    telefono: string;
    genero: string;
    fechaNacimiento: string;
    fechaCreacion?: string;
}