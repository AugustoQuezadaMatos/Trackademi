import { Component, AfterViewInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { PanelMenuModule } from 'primeng/panelmenu';
import { CommonModule } from '@angular/common';
import { MenubarModule } from 'primeng/menubar';
import { MenuItem } from 'primeng/api';
import { RouterLink } from '@angular/router';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';


@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    SidebarModule,
    ButtonModule,
    PanelMenuModule,

    MenubarModule, AvatarModule, AvatarGroupModule
  ],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements AfterViewInit {
  menuItems: MenuItem[] = [
    { label: 'Usuarios', icon: 'pi pi-users', routerLink: '/usuarios' },
    { label: 'Estudiantes', icon: 'pi pi-user', routerLink: '/estudiantes' },
    { label: 'Materias', icon: 'pi pi-bookmark', routerLink: '/materias' },
    { label: 'Calificaciones', icon: 'pi pi-book', routerLink: '/calificaciones' },
    { label: 'Periodos', icon: 'pi pi-book', routerLink: '/periodos' },
    { label: 'Asistencia', icon: 'pi pi-book', routerLink: '/control-asistencia' }
  ];

  sidebarVisible: boolean = true;
  isMobile: boolean = false;

  ngAfterViewInit() {
    this.checkScreenSize();
    if (typeof window !== 'undefined') {
      window.addEventListener('resize', this.checkScreenSize.bind(this));
    }
  }

  toggleSidebar() {
    this.sidebarVisible = !this.sidebarVisible;
  }

  checkScreenSize() {
    if (typeof window !== 'undefined') {
      this.isMobile = window.innerWidth < 768;
      this.sidebarVisible = !this.isMobile;
    }
  }
}
