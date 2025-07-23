import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalificacionExtendida } from '../../models/calificacion-extendida.model';
import { CalificacionesService } from '../../services/calificaciones.service';
import { TableModule } from 'primeng/table';
import { Table } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { confirmarAccionObservable } from '../../helpers/ui.helpers';
import { MessageService, ConfirmationService } from 'primeng/api';
import { LiteralPipe } from '../../helpers/literal.pipe';
import { DropdownModule } from 'primeng/dropdown';
import { MateriasService } from '../../services/materias.service';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-calificaciones',
    standalone: true,
    templateUrl: './calificaciones.component.html',
    styleUrls: ['./calificaciones.component.scss'],
    imports: [
        CommonModule,
        TableModule,
        InputTextModule,
        ButtonModule,
        InputNumberModule,
        LiteralPipe,
        DropdownModule,
        FormsModule
    ]
})
export class CalificacionesComponent implements OnInit {
    calificaciones: CalificacionExtendida[] = [];
    erroresValidacion: { [key: string]: string[] } = {};
    modoEdicionFilaId: number | null = null;

    materias: any[] = [];
    materiaSeleccionada: any = null;

    @ViewChild('dt') dt!: Table;

    constructor(
        private calificacionesService: CalificacionesService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private MateriasService: MateriasService
    ) { }

    ngOnInit(): void {
        this.cargarMaterias();
    }

    cargarMaterias() {
        this.MateriasService.getMaterias().subscribe({
            next: (res) => {
                if (res.success) this.materias = res.data;
            },
            error: (err) => console.error('Error al cargar materias', err)
        });
    }

    onMateriaSeleccionada() {
        if (!this.materiaSeleccionada) return;

        this.calificacionesService.getExtendidasPorMateria(this.materiaSeleccionada.id).subscribe({
            next: (res) => {
                if (res.success) {
                    this.calificaciones = res.data.map((c: any) => ({
                        ...c,
                        idCalificacion: c.idCalificacion ?? 0,
                        idMateria: c.idMateria ?? this.materiaSeleccionada.id,
                        idEstudiante: c.idEstudiante,
                        notaParcial: c.notaParcial ?? 0,
                        notaFinal: c.notaFinal ?? 0,
                        observaciones: c.observaciones ?? '',
                        nombreCompleto: `${c.nombres} ${c.apellidos}`
                    }));
                } else {
                    this.calificaciones = [];
                }
            },
            error: (err) => {
                console.error('Error al cargar calificaciones extendidas', err);
                this.calificaciones = [];
            }
        });
    }


    guardarTodas() {
        console.log('Calificaciones a guardar:', this.calificaciones);
        if (!this.materiaSeleccionada) {
            this.messageService.add({
                severity: 'warn',
                summary: 'Materia no seleccionada',
                detail: 'Debes seleccionar una materia antes de guardar.'
            });
            return;
        }

        const tieneCamposInvalidos = this.calificaciones.some(c =>
            c.notaParcial === null || c.notaParcial === undefined
        );

        if (tieneCamposInvalidos) {
            this.messageService.add({
                severity: 'error',
                summary: 'Campos incompletos',
                detail: 'Todas las calificaciones deben tener nota parcial.'
            });
            return;
        }


        this.calificaciones = this.calificaciones.map(c => ({
            ...c,
            idCalificacion: c.idCalificacion ?? 0,
            notaParcial: c.notaParcial ?? 0,
            notaFinal: c.notaFinal ?? 0,
            observaciones: c.observaciones ?? ''
        }));
        console.log(this.calificaciones)
        confirmarAccionObservable(this.confirmationService, this.messageService, {
            mensaje: '¿Deseas guardar todas las calificaciones para esta materia?',
            exito: 'Calificaciones guardadas correctamente',
            fallo: 'No se pudieron guardar las calificaciones',
            cancelado: 'Acción cancelada',
            onConfirmar: () =>
                this.calificacionesService.guardarLote(this.calificaciones),
            onSuccess: () => this.onMateriaSeleccionada(),
            onErrorValidacion: (errors) => {
                this.erroresValidacion = errors || {};
            }
        });
    }


    guardarFila(calif: CalificacionExtendida) {

        const califNormalizada: CalificacionExtendida = {
            ...calif,
            idCalificacion: calif.idCalificacion ?? 0,
            notaParcial: calif.notaParcial ?? 0,
            notaFinal: calif.notaFinal ?? 0,
            observaciones: calif.observaciones ?? ''
        };

        confirmarAccionObservable(this.confirmationService, this.messageService, {
            mensaje: '¿Deseas guardar los cambios de esta calificación?',
            exito: 'Calificación actualizada correctamente',
            fallo: 'No se pudo actualizar la calificación',
            cancelado: 'Acción cancelada',
            onConfirmar: () => this.calificacionesService.actualizar(califNormalizada),
            onSuccess: () => {
                this.modoEdicionFilaId = null;
                this.onMateriaSeleccionada();
            },
            onErrorValidacion: (errors) => {
                this.erroresValidacion = errors || {};
            }
        });
    }


    eliminarCalificacion(id: number) {
        confirmarAccionObservable(this.confirmationService, this.messageService, {
            mensaje: '¿Eliminar esta calificación?',
            exito: 'Calificación eliminada correctamente',
            fallo: 'No se pudo eliminar la calificación',
            cancelado: 'Eliminación cancelada',
            onConfirmar: () => this.calificacionesService.eliminar(id),
            onSuccess: () => this.onMateriaSeleccionada()
        });
    }

    buscarGlobal(event: Event) {
        const input = event.target as HTMLInputElement;
        this.dt.filterGlobal(input.value, 'contains');
    }
}
