import { Component, OnInit } from '@angular/core';
import { PeriodosService } from '../../services/periodos.service';
import { Periodo } from '../../models/periodo.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';


@Component({
    standalone: true,
    selector: 'app-periodos',
    imports: [
        FormsModule,
        CommonModule,
        TableModule,
        ButtonModule,
        InputTextModule
    ],
    templateUrl: './periodos.component.html'
})
export class PeriodosComponent implements OnInit {
    periodos: Periodo[] = [];
    nuevoPeriodo: Periodo = {
        idPeriodo: 0,
        codigo: '',
        // fechaCreacion: new Date(),
        idUsuario: 1 // ← Ajusta según lógica real
    };

    constructor(private periodosService: PeriodosService) { }

    ngOnInit() {
        this.cargarPeriodos();
    }

    cargarPeriodos() {
        this.periodosService.getAll().subscribe({
            next: res => this.periodos = res.data,
            error: err => console.error('Error al cargar períodos', err)
        });
    }

    guardar(): void {
        this.periodosService.create(this.nuevoPeriodo).subscribe({
            next: () => {
                this.cargarPeriodos(); // Refresca la tabla
                this.nuevoPeriodo.codigo = '';
            },
            error: (err) => console.error('Error al guardar periodo', err),
        });
    }

    eliminar(id: number) {
        this.periodosService.delete(id).subscribe({
            next: () => this.cargarPeriodos(),
            error: err => console.error('Error al eliminar período', err)
        });
    }
}
