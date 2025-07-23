import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Periodo } from '../models/periodo.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class PeriodosService {
    private apiUrl = 'https://localhost:7144/api/periodos';

    constructor(private http: HttpClient) { }

    getAll(): Observable<any> {
        return this.http.get(this.apiUrl);
    }

    getById(id: number): Observable<any> {
        return this.http.get(`${this.apiUrl}/${id}`);
    }

    create(periodo: Periodo): Observable<any> {
        return this.http.post(this.apiUrl, periodo);
    }

    update(periodo: Periodo): Observable<any> {
        return this.http.put(this.apiUrl, periodo);
    }

    delete(id: number): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
}
