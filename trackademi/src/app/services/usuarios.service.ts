import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationResult } from '../models/operation-result.model';
import { Usuario } from '../models/usuario.model';
import { Perfil } from '../models/perfil.model';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {
  private apiUrl = 'https://localhost:7144/api/usuarios';
  // Ajusta el puerto si es necesario

  constructor(private http: HttpClient) { }


  getUsuarios(): Observable<OperationResult<Usuario[]>> {
    return this.http.get<OperationResult<Usuario[]>>(this.apiUrl);
  }

  getPerfiles(): Observable<OperationResult<Perfil[]>> {
    return this.http.get<OperationResult<Perfil[]>>('https://localhost:7144/api/perfiles');
  }

  eliminarUsuario(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  crearUsuario(usuario: Usuario): Observable<OperationResult<null>> {
    console.log("despues de validar en crear usuario")
    return this.http.post<OperationResult<null>>(this.apiUrl, usuario);
  }

  actualizarUsuario(usuario: Usuario): Observable<OperationResult<null>> {
    return this.http.put<OperationResult<null>>(this.apiUrl, usuario);
  }

}
