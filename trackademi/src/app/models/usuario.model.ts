export interface Usuario {
    idUsuario?: number;
    nombres?: string;
    apellidos?: string;
    correo?: string;
    usuario?: string;
    clave?: string;
    idPerfil?: number;
    activo?: boolean;
    fechaCreacion?: string; // puede ser string si es ISO
}
