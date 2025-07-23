import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Estudiante } from '../../models/estudiante.model';
import { EstudiantesService } from '../../services/estudiantes.service';
import { TableModule } from 'primeng/table';
import { Table } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { FormsModule, NgForm } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { InputMaskModule } from 'primeng/inputmask';
import { confirmarAccionObservable } from '../../helpers/ui.helpers';
import { MessageService, ConfirmationService } from 'primeng/api';

@Component({
    selector: 'app-estudiantes',
    standalone: true,
    imports: [
        CommonModule,
        TableModule,
        InputTextModule,
        ButtonModule,
        DialogModule,
        FormsModule,
        DropdownModule,
        CalendarModule,
        InputMaskModule
    ],
    templateUrl: './estudiantes.component.html',
    styleUrls: ['./estudiantes.component.scss']
})
export class EstudiantesComponent implements OnInit {
    estudiantes: Estudiante[] = [];
    nuevoEstudiante: Partial<Estudiante> = {};
    erroresValidacion: { [key: string]: string[] } = {};
    crearDialogVisible = false;
    modoEdicion = false;

    generos = [
        { label: 'Masculino', value: 'M' },
        { label: 'Femenino', value: 'F' }
    ];

    @ViewChild('dt') dt!: Table;

    constructor(
        private estudiantesService: EstudiantesService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) { }

    ngOnInit(): void {
        this.cargarEstudiantes();
    }

    cargarEstudiantes() {
        this.estudiantesService.getEstudiantes().subscribe({
            next: (res) => {
                if (res.success) {
                    this.estudiantes = res.data;
                }
            },
            error: (err) => console.error('Error al cargar estudiantes', err)
        });
    }

    buscarGlobal(event: Event) {
        const input = event.target as HTMLInputElement;
        this.dt.filterGlobal(input.value, 'contains');
    }

    abrirDialogCrear() {
        this.nuevoEstudiante = {};
        this.modoEdicion = false;
        this.erroresValidacion = {};
        this.crearDialogVisible = true;
    }

    abrirDialogEditar(estudiante: Estudiante) {
        this.nuevoEstudiante = { ...estudiante };
        this.modoEdicion = true;
        this.erroresValidacion = {};
        this.crearDialogVisible = true;
    }

    guardarEstudiante(form: NgForm) {
        this.erroresValidacion = {};
        (form as any).submitted = true;
        form.control.markAllAsTouched();

        if (!form.valid) return;
        if (this.nuevoEstudiante.fechaNacimiento) {
            const raw = this.nuevoEstudiante.fechaNacimiento;
            const fecha = new Date(raw);
            if (!isNaN(fecha.getTime())) {
                this.nuevoEstudiante.fechaNacimiento = fecha.toISOString().split('T')[0];
            }
        }


        confirmarAccionObservable(this.confirmationService, this.messageService, {
            mensaje: this.modoEdicion ? '¿Deseas actualizar este estudiante?' : '¿Deseas registrar este estudiante?',
            exito: this.modoEdicion ? 'Estudiante actualizado correctamente' : 'Estudiante registrado correctamente',
            fallo: 'No se pudo guardar el estudiante',
            cancelado: 'Acción cancelada',
            onConfirmar: () =>
                this.modoEdicion
                    ? this.estudiantesService.actualizarEstudiante(this.nuevoEstudiante as Estudiante)
                    : this.estudiantesService.crearEstudiante(this.nuevoEstudiante as Estudiante),
            onSuccess: () => {
                this.crearDialogVisible = false;
                this.nuevoEstudiante = {};
                this.cargarEstudiantes();
            },
            onErrorValidacion: (errors) => {
                this.erroresValidacion = errors || {};
            }
        });
    }

    eliminarEstudiante(id: number) {
        confirmarAccionObservable(this.confirmationService, this.messageService, {
            mensaje: '¿Deseas eliminar este estudiante?',
            exito: 'Estudiante eliminado correctamente',
            fallo: 'No se pudo eliminar el estudiante',
            cancelado: 'Eliminación cancelada',
            onConfirmar: () => this.estudiantesService.eliminarEstudiante(id),
            onSuccess: () => this.cargarEstudiantes()
        });
    }
}
