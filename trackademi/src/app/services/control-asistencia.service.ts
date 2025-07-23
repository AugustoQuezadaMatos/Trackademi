import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { confirmarAccionObservable } from '../helpers/ui.helpers';
import { ConfirmationService, MessageService } from 'primeng/api';
import { OperationResult } from '../models/operation-result.model';
import { Estudiante } from '../models/estudiante.model';
import { ControlAsistenciaDto } from '../models/control-asistencia.model';


@Injectable({
    providedIn: 'root',
})
export class ControlAsistenciaService {
    private baseUrl = 'https://localhost:7144/api'; // Ajustar a tu backend

    constructor(private http: HttpClient,
        private confirmationService: ConfirmationService,
        private messageService: MessageService
    ) {

    }

    obtenerMaterias(): Observable<any> {
        return this.http.get(`${this.baseUrl}/materias`);
    }

    obtenerEstudiantesPorMateria(id: number): Observable<any> {
        return this.http.get(`${this.baseUrl}/materias/estudiantes-asociados/${id}`);
    }


    obtenerHistorialAsistencia(idMateria: number): Observable<any> {
        return this.http.get(`${this.baseUrl}/controlasistencia/historial/${idMateria}`);
    }

    obtenerEstudiantesConDetalle(idMateria: number): Observable<OperationResult<Estudiante[]>> {
        return this.http.get<OperationResult<Estudiante[]>>(`${this.baseUrl}/controlasistencia/estudiantes/${idMateria}`);
    }


    registrarAsistencia(data: ControlAsistenciaDto[]): Observable<OperationResult<null>> {
        return this.http.post<OperationResult<null>>(`${this.baseUrl}/controlasistencia`, data);
    }

}
