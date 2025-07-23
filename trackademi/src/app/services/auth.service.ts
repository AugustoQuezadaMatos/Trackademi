import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { LoginModel } from '../models/login.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private baseUrl = 'https://localhost:7144/api/auth';

    constructor(private http: HttpClient) { }

    login(data: LoginModel): Observable<{ token: string }> {
        return this.http.post<{ token: string }>(`${this.baseUrl}/login`, data).pipe(
            tap(res => {
                localStorage.setItem('token', res.token);
            })
        );
    }

    logout() {
        localStorage.removeItem('token');
    }

    isAuthenticated(): boolean {
        return !!localStorage.getItem('token');
    }

    getToken(): string | null {
        return localStorage.getItem('token');
    }


}
