import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { AuthGuard } from './pages/auth/auth.guard';

export const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        canActivate: [AuthGuard],
        children: [
            {
                path: 'usuarios',
                loadComponent: () =>
                    import('./pages/usuarios/usuarios.component').then(
                        (m) => m.UsuariosComponent
                    ),
            },
            {
                path: 'estudiantes',
                loadComponent: () =>
                    import('./pages/estudiantes/estudiantes.component').then(
                        (m) => m.EstudiantesComponent
                    ),
            },
            {
                path: 'materias',
                loadComponent: () =>
                    import('./pages/materias/materias.component').then(
                        (m) => m.MateriasComponent
                    ),
            },
            {
                path: 'calificaciones',
                loadComponent: () =>
                    import('./pages/calificaciones/calificaciones.component').then(
                        (m) => m.CalificacionesComponent
                    ),
            },
            {
                path: 'periodos',
                loadComponent: () =>
                    import('./pages/periodos/periodos.component').then(
                        (m) => m.PeriodosComponent
                    ),
            },
            {
                path: 'control-asistencia',
                loadComponent: () =>
                    import('./pages/control-asistencia/control-asistencia.component').then(
                        (m) => m.ControlAsistenciaComponent
                    ),
            },
        ],
    },
    {
        path: 'login',
        loadComponent: () =>
            import('./pages/login/login.component').then(
                (m) => m.LoginComponent
            ),
    },
    { path: '**', redirectTo: '' },
];

