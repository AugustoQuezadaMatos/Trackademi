import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationResult } from '../models/operation-result.model';
import { Calificacion } from '../models/calificacion.model';
import { CalificacionExtendida } from '../models/calificacion-extendida.model';
@Injectable({
    providedIn: 'root'
})
export class CalificacionesService {
    private apiUrl = 'https://localhost:7144/api/calificaciones';

    constructor(private http: HttpClient) { }

    getTodas(): Observable<OperationResult<Calificacion[]>> {
        return this.http.get<OperationResult<Calificacion[]>>(this.apiUrl);
    }

    getTodasExtendidas(): Observable<OperationResult<CalificacionExtendida[]>> {
        return this.http.get<OperationResult<CalificacionExtendida[]>>(`${this.apiUrl}/extendidas`);
    }

    getPorId(id: number): Observable<OperationResult<Calificacion>> {
        return this.http.get<OperationResult<Calificacion>>(`${this.apiUrl}/${id}`);
    }

    crear(calificacion: Calificacion): Observable<OperationResult<null>> {
        return this.http.post<OperationResult<null>>(this.apiUrl, calificacion);
    }

    actualizar(calificacion: Calificacion): Observable<OperationResult<null>> {
        return this.http.put<OperationResult<null>>(this.apiUrl, calificacion);
    }

    eliminar(id: number): Observable<OperationResult<null>> {
        return this.http.delete<OperationResult<null>>(`${this.apiUrl}/${id}`);
    }

    obtenerLiteral(notaFinal: number): Observable<OperationResult<{ notaFinal: number; literal: string }>> {
        return this.http.get<OperationResult<{ notaFinal: number; literal: string }>>(`${this.apiUrl}/literal/${notaFinal}`);
    }

    guardarLote(calificaciones: Calificacion[]): Observable<OperationResult<null>> {
        return this.http.post<OperationResult<null>>(`${this.apiUrl}/lote`, calificaciones);
    }

    getPorMateria(idMateria: number): Observable<OperationResult<Calificacion[]>> {
        return this.http.get<OperationResult<Calificacion[]>>(`${this.apiUrl}/materia/${idMateria}`);
    }

    getExtendidasPorMateria(idMateria: number): Observable<OperationResult<CalificacionExtendida[]>> {
        return this.http.get<OperationResult<CalificacionExtendida[]>>(`${this.apiUrl}/completo/materia/${idMateria}`);
    }

}
