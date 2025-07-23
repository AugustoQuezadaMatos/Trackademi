import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Usuario } from '../../models/usuario.model';
import { UsuariosService } from '../../services/usuarios.service';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { FormsModule, NgForm } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { Perfil } from '../../models/perfil.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { confirmarAccionObservable } from '../../helpers/ui.helpers';
import { PanelMenuModule } from "primeng/panelmenu";
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule,
    TableModule,
    InputTextModule,
    ButtonModule,
    DialogModule,
    FormsModule,
    DropdownModule, PanelMenuModule, CardModule],
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss']
})
export class UsuariosComponent implements OnInit {
  usuarios: Usuario[] = [];
  perfiles: Perfil[] = [];
  crearDialogVisible = false;
  erroresValidacion: { [key: string]: string[] } = {};
  modoEdicion: boolean = false;

  @ViewChild('dt') dt!: Table; // para el filtro que tenemos en la tabla
  constructor(private usuariosService: UsuariosService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) { }

  nuevoUsuario: Partial<Usuario> = {
    idUsuario: 0,
    nombres: '',
    apellidos: '',
    correo: '',
    usuario: '',
    clave: '',
    idPerfil: 0,
    activo: true
  };


  ngOnInit(): void {
    this.cargarUsuarios();

    this.usuariosService.getPerfiles().subscribe({
      next: (res) => {
        if (res.success) {
          this.perfiles = res.data;
        }
      },
      error: (err) => console.error('Error al obtener perfiles', err)
    });
  }
  buscarGlobal(event: Event) {
    const input = event.target as HTMLInputElement;
    this.dt?.filterGlobal(input.value, 'contains');
  }

  guardarUsuario(form: NgForm) {
    this.erroresValidacion = {};
    (form as any).submitted = true;
    form.control.markAllAsTouched();
    console.log("antes de validar")

    if (!form.valid) return;
    console.log("despues de validar")
    confirmarAccionObservable(this.confirmationService, this.messageService, {
      mensaje: this.modoEdicion
        ? '¿Deseas guardar los cambios del usuario?'
        : '¿Deseas crear este nuevo usuario?',
      exito: this.modoEdicion
        ? 'Usuario actualizado correctamente'
        : 'Usuario creado correctamente',
      fallo: 'No se pudo guardar el usuario',
      cancelado: 'Acción cancelada',
      onConfirmar: () =>
        this.modoEdicion
          ? this.usuariosService.actualizarUsuario(this.nuevoUsuario)
          : this.usuariosService.crearUsuario(this.nuevoUsuario),
      onSuccess: () => {
        this.crearDialogVisible = false;
        this.nuevoUsuario = {};
        this.cargarUsuarios();
      },
      onErrorValidacion: (errors) => {
        this.erroresValidacion = errors || {};
        console.log(this.erroresValidacion)
      }
    });

  }



  abrirDialogEditar(usuario: Usuario) {
    this.nuevoUsuario = { ...usuario };
    this.modoEdicion = true;
    this.erroresValidacion = {};
    this.crearDialogVisible = true;
  }

  abrirDialogCrear() {
    this.nuevoUsuario = {}; // o usa: {} as Usuario
    this.modoEdicion = false;
    this.erroresValidacion = {};
    this.crearDialogVisible = true;
  }



  eliminarUsuario(id: number) {
    confirmarAccionObservable(this.confirmationService, this.messageService, {
      mensaje: '¿Estás seguro de eliminar este usuario?',
      exito: 'Usuario eliminado correctamente',
      fallo: 'No se pudo eliminar el usuario',
      cancelado: 'Eliminación cancelada',
      onConfirmar: () => this.usuariosService.eliminarUsuario(id),
      onSuccess: () => this.cargarUsuarios()
    });
  }


  cargarUsuarios() {
    this.usuariosService.getUsuarios().subscribe({
      next: (res) => {
        if (res.success) {
          this.usuarios = res.data;
        }
      },
      error: (err) => console.error('Error al cargar usuarios:', err)
    });
  }




}
