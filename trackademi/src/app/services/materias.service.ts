import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Materia } from '../models/materia.model';
import { OperationResult } from '../models/operation-result.model';

@Injectable({
    providedIn: 'root'
})
export class MateriasService {
    private apiUrl = 'https://localhost:7144/api/materias';

    constructor(private http: HttpClient) { }

    getMaterias(): Observable<OperationResult<Materia[]>> {
        return this.http.get<OperationResult<Materia[]>>(this.apiUrl);
    }

    getMateriaPorId(id: number): Observable<OperationResult<Materia>> {
        return this.http.get<OperationResult<Materia>>(`${this.apiUrl}/${id}`);
    }

    crearMateria(materia: Materia): Observable<OperationResult<null>> {
        return this.http.post<OperationResult<null>>(this.apiUrl, materia);
    }

    actualizarMateria(materia: Materia): Observable<OperationResult<null>> {
        return this.http.put<OperationResult<null>>(this.apiUrl, materia);
    }

    eliminarMateria(id: number): Observable<OperationResult<null>> {
        return this.http.delete<OperationResult<null>>(`${this.apiUrl}/${id}`);
    }

    asociarEstudiantes(idMateria: number, idsEstudiantes: number[]): Observable<OperationResult<null>> {
        return this.http.put<OperationResult<null>>(
            `${this.apiUrl}/asociar-estudiantes/${idMateria}`,
            idsEstudiantes
        );
    }

    getEstudiantesAsociados(idMateria: number): Observable<OperationResult<number[]>> {
        return this.http.get<OperationResult<number[]>>(`${this.apiUrl}/estudiantes-asociados/${idMateria}`);
    }

    getCantidadEstudiantes(idMateria: number): Observable<OperationResult<number>> {
        return this.http.get<OperationResult<number>>(`${this.apiUrl}/cantidad-estudiantes/${idMateria}`);
    }


}
