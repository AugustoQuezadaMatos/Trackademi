import { Component, OnInit } from '@angular/core';
import { ControlAsistenciaService } from '../../services/control-asistencia.service';
import { Estudiante } from '../../models/estudiante.model';
import { Materia } from '../../models/materia.model';
import { confirmarAccionObservable } from '../../helpers/ui.helpers';
import { ConfirmationService, MessageService } from 'primeng/api';
import { OperationResult } from '../../models/operation-result.model';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';

@Component({
    standalone: true,
    selector: 'app-control-asistencia',
    imports: [
        FormsModule,
        DropdownModule,
        CalendarModule,
        CheckboxModule,
        ButtonModule,
        TableModule,
        CommonModule,
        DialogModule
    ],
    templateUrl: './control-asistencia.component.html',
    styleUrls: ['./control-asistencia.component.scss']
})
export class ControlAsistenciaComponent implements OnInit {
    materias: Materia[] = [];
    estudiantes: Estudiante[] = [];
    fechaAsistencia: Date = new Date();
    materiaSeleccionada?: Materia;
    asistencia: { [idEstudiante: number]: boolean } = {};

    constructor(private asistenciaService: ControlAsistenciaService,
        private confirmationService: ConfirmationService,
        private messageService: MessageService
    ) { }

    historialDialogVisible = false;
    historialAsistencia: {
        idEstudiante: number;
        nombres: string;
        apellidos: string;
        matricula: string;
        fecha: string;
        presente: boolean;
    }[] = [];

    ngOnInit() {
        this.cargarMaterias();
    }

    cargarMaterias() {
        this.asistenciaService.obtenerMaterias().subscribe((res) => {
            this.materias = res.data;
        });
    }
    cargarEstudiantes(idMateria?: number) {
        if (!idMateria) return;

        this.asistenciaService.obtenerEstudiantesConDetalle(idMateria).subscribe((res) => {
            this.estudiantes = res.data;
            this.asistencia = {}; // resetear asistencias
        });
    }


    guardarAsistencia() {
        const registros = this.estudiantes.map(est => ({
            idEstudiante: est.idEstudiante,
            idMateria: this.materiaSeleccionada?.id!,
            presente: this.asistencia[est.idEstudiante] || false,
            fecha: this.fechaAsistencia.toISOString().split('T')[0]
        }));

        confirmarAccionObservable<OperationResult<any>>(
            this.confirmationService,
            this.messageService,
            {
                mensaje: '¿Deseas guardar esta asistencia?',
                exito: 'Asistencia registrada',
                fallo: 'Error al guardar asistencia',
                cancelado: 'Operación cancelada',
                onConfirmar: () => this.asistenciaService.registrarAsistencia(registros),
                onSuccess: () => {
                    this.asistencia = {};
                    this.estudiantes = [];
                    this.materiaSeleccionada = undefined;
                }
            }
        );
    }


    verHistorial() {
        if (!this.materiaSeleccionada?.id) return;

        this.asistenciaService.obtenerHistorialAsistencia(this.materiaSeleccionada.id).subscribe(res => {
            this.historialAsistencia = res.data;
            this.historialDialogVisible = true;
        });
    }




}
