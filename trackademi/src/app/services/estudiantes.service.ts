import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationResult } from '../models/operation-result.model';
import { Estudiante } from '../models/estudiante.model';

@Injectable({
    providedIn: 'root'
})
export class EstudiantesService {
    private apiUrl = 'https://localhost:7144/api/estudiantes';

    constructor(private http: HttpClient) { }

    getEstudiantes(): Observable<OperationResult<Estudiante[]>> {
        return this.http.get<OperationResult<Estudiante[]>>(this.apiUrl);
    }

    buscarPorNombre(nombre: string): Observable<OperationResult<Estudiante[]>> {
        return this.http.get<OperationResult<Estudiante[]>>(`${this.apiUrl}/buscar?nombre=${nombre}`);
    }

    crearEstudiante(estudiante: Estudiante): Observable<OperationResult<null>> {
        return this.http.post<OperationResult<null>>(this.apiUrl, estudiante);
    }

    actualizarEstudiante(estudiante: Estudiante): Observable<OperationResult<null>> {
        return this.http.put<OperationResult<null>>(this.apiUrl, estudiante);
    }

    eliminarEstudiante(id: number): Observable<OperationResult<null>> {
        return this.http.delete<OperationResult<null>>(`${this.apiUrl}/${id}`);
    }

    getPorId(id: number): Observable<OperationResult<Estudiante>> {
        return this.http.get<OperationResult<Estudiante>>(`${this.apiUrl}/${id}`);
    }
}
