import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  menuItems = [
    { path: '/dashboard', label: 'Dashboard', icon: 'home' },
    { path: '/cats', label: 'Meus Gatos', icon: 'pets' },
    { path: '/vaccines', label: 'Vacinas', icon: 'medical' },
    { path: '/stock', label: 'Estoque', icon: 'inventory' },
    { path: '/finance', label: 'Financeiro', icon: 'money' },
    { path: '/wishlist', label: 'Lista de Desejos', icon: 'list' }
  ];
}
