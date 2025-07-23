import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MateriasService } from '../../services/materias.service';
import { Materia } from '../../models/materia.model';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, NgForm } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { confirmarAccionObservable } from '../../helpers/ui.helpers';
import { Table } from 'primeng/table';
import { EstudiantesService } from '../../services/estudiantes.service';
import { Estudiante } from '../../models/estudiante.model';
import { MultiSelectModule } from 'primeng/multiselect';
import { PeriodosService } from '../../services/periodos.service';
import { DropdownModule } from 'primeng/dropdown';

@Component({
    selector: 'app-materias',
    standalone: true,
    imports: [
        CommonModule,
        TableModule,
        ButtonModule,
        DialogModule,
        InputTextModule,
        FormsModule,
        MultiSelectModule,
        DropdownModule
    ],

    templateUrl: './materias.component.html',
    styleUrls: ['./materias.component.scss']
})
export class MateriasComponent implements OnInit {
    materias: Materia[] = [];
    nuevaMateria: Partial<Materia> = {};
    erroresValidacion: { [key: string]: string[] } = {};
    crearDialogVisible = false;
    modoEdicion = false;

    asociarDialogVisible = false;
    periodos: any[] = [];

    todosEstudiantes: Estudiante[] = [];
    estudiantesSeleccionados: Estudiante[] = [];


    @ViewChild('dt') dt!: Table;

    constructor(
        private materiasService: MateriasService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private estudiantesService: EstudiantesService,
        private periodosService: PeriodosService


    ) { }

    materiaSeleccionada: Materia = {
        id: 0,
        nombre: '',
        detalle: '',
        idPeriodo: 0
    };

    ngOnInit(): void {
        this.cargarMaterias();
        this.cargarPeriodos();
    }

    cargarMaterias() {
        this.materiasService.getMaterias().subscribe({
            next: (res) => {
                if (res.success) this.materias = res.data;
            },
            error: (err) => console.error('Error al cargar materias', err)
        });
    }
    getNombrePeriodo(idPeriodo: number): string {
        const periodo = this.periodos.find(p => p.idPeriodo === idPeriodo);
        return periodo ? periodo.codigo : 'Sin período';
    }
    cargarPeriodos() {
        this.periodosService.getAll().subscribe({
            next: (res) => {
                this.periodos = res.data;
            },
            error: (err) => {
                console.error('Error al cargar períodos', err);
            },
        });
    }

    buscarGlobal(event: Event) {
        const input = event.target as HTMLInputElement;
        this.dt?.filterGlobal(input.value, 'contains');
    }

    abrirDialogCrear() {
        this.nuevaMateria = {};
        this.modoEdicion = false;
        this.crearDialogVisible = true;
        this.erroresValidacion = {};
    }

    abrirDialogEditar(materia: Materia) {
        this.nuevaMateria = { ...materia };
        this.modoEdicion = true;
        this.crearDialogVisible = true;
        this.erroresValidacion = {};
    }

    guardarMateria(form: NgForm) {
        this.erroresValidacion = {};
        (form as any).submitted = true;
        form.control.markAllAsTouched();
        if (!form.valid) return;

        confirmarAccionObservable(this.confirmationService, this.messageService, {
            mensaje: this.modoEdicion
                ? '¿Deseas actualizar esta materia?'
                : '¿Deseas crear esta nueva materia?',
            exito: this.modoEdicion
                ? 'Materia actualizada correctamente'
                : 'Materia creada correctamente',
            fallo: 'No se pudo guardar la materia',
            cancelado: 'Acción cancelada',
            onConfirmar: () =>
                this.modoEdicion
                    ? this.materiasService.actualizarMateria(this.nuevaMateria as Materia)
                    : this.materiasService.crearMateria(this.nuevaMateria as Materia),
            onSuccess: () => {
                this.crearDialogVisible = false;
                this.nuevaMateria = {};
                this.cargarMaterias();
            },
            onErrorValidacion: (errors) => {
                this.erroresValidacion = errors || {};
            }
        });
    }

    eliminarMateria(id: number) {
        confirmarAccionObservable(this.confirmationService, this.messageService, {
            mensaje: '¿Estás seguro de eliminar esta materia?',
            exito: 'Materia eliminada correctamente',
            fallo: 'No se pudo eliminar la materia',
            cancelado: 'Eliminación cancelada',
            onConfirmar: () => this.materiasService.eliminarMateria(id),
            onSuccess: () => this.cargarMaterias()
        });
    }

    abrirModalAsociarEstudiantes(materia: Materia) {
        this.materiaSeleccionada = materia;
        this.asociarDialogVisible = true;

        this.estudiantesService.getEstudiantes().subscribe({
            next: (res) => {
                if (res.success) {
                    this.todosEstudiantes = res.data;

                    // 🔄 Precargar estudiantes asociados
                    this.materiasService.getEstudiantesAsociados(materia.id).subscribe({
                        next: (resAsociados) => {
                            if (resAsociados.success) {
                                const idsAsociados = resAsociados.data;
                                this.estudiantesSeleccionados = this.todosEstudiantes.filter(est =>
                                    idsAsociados.includes(est.idEstudiante));
                            }
                        },
                        error: (err) => console.error('Error al precargar asociados', err)
                    });
                }
            },
            error: (err) => console.error('Error al obtener estudiantes', err)
        });
    }


    guardarAsociaciones() {

        console.log('Materia seleccionada:', this.materiaSeleccionada);
        console.log('ID Materia:', this.materiaSeleccionada?.id);
        if (!this.materiaSeleccionada) return;

        const idsEstudiantes = this.estudiantesSeleccionados.map(e => e.idEstudiante);

        this.materiasService.asociarEstudiantes(this.materiaSeleccionada.id, idsEstudiantes).subscribe({
            next: (res) => {
                if (res.success) {
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Asociado',
                        detail: res.message || 'Estudiantes asociados correctamente',
                    });
                    this.asociarDialogVisible = false;
                } else {
                    this.messageService.add({
                        severity: 'warn',
                        summary: 'Advertencia',
                        detail: res.message || 'No se pudieron asociar los estudiantes',
                    });
                }
            },
            error: (err) => {
                console.error('Error al asociar estudiantes', err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Ocurrió un error inesperado al asociar estudiantes',
                });
            }
        });
    }


}
