import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoginModel } from '../../models/login.model';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { FormsModule } from '@angular/forms'; // 👈 necesario
import { RouterModule } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';

@Component({
    selector: 'app-login',
    standalone: true,
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        ButtonModule,
        CardModule,
        InputTextModule
    ]
})
export class LoginComponent {
    loginData: LoginModel = { usuario: '', clave: '' };
    error: string = '';

    constructor(private auth: AuthService, private router: Router) { }

    ingresar() {
        this.auth.login(this.loginData).subscribe({
            next: () => this.router.navigate(['/']),
            error: () => this.error = 'Credenciales inválidas'
        });
    }
}
