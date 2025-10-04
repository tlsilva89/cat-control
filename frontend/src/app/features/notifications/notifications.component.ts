import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NotificationsService } from '../../core/services/notifications.service';
import { Notification } from '../../core/models/notification.model';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent implements OnInit {
  notifications: Notification[] = [];
  loading = true;
  selectedFilter = 'all';

  constructor(private notificationsService: NotificationsService) {}

  ngOnInit(): void {
    this.loadNotifications();
  }

  loadNotifications(): void {
    this.loading = true;

    if (this.selectedFilter === 'all') {
      this.notificationsService.getNotifications().subscribe({
        next: (notifications) => {
          this.notifications = notifications;
          this.loading = false;
        }
      });
    } else if (this.selectedFilter === 'unread') {
      this.notificationsService.getUnreadNotifications().subscribe({
        next: (notifications) => {
          this.notifications = notifications;
          this.loading = false;
        }
      });
    } else {
      this.notificationsService.getNotifications(true).subscribe({
        next: (notifications) => {
          this.notifications = notifications;
          this.loading = false;
        }
      });
    }
  }

  filterNotifications(filter: string): void {
    this.selectedFilter = filter;
    this.loadNotifications();
  }

  markAsRead(id: number): void {
    this.notificationsService.markAsRead(id).subscribe({
      next: () => {
        this.loadNotifications();
      }
    });
  }

  markAllAsRead(): void {
    this.notificationsService.markAllAsRead().subscribe({
      next: () => {
        this.loadNotifications();
      }
    });
  }

  deleteNotification(id: number): void {
    if (confirm('Tem certeza que deseja excluir esta notificação?')) {
      this.notificationsService.deleteNotification(id).subscribe({
        next: () => {
          this.loadNotifications();
        }
      });
    }
  }

  getPriorityClass(prioridade: string, lida: boolean): string {
    const baseClass = lida ? 'bg-gray-50' : '';
    
    switch (prioridade) {
      case 'Alta':
        return lida ? baseClass : 'bg-red-50 border-l-4 border-red-600';
      case 'Normal':
        return lida ? baseClass : 'bg-yellow-50 border-l-4 border-yellow-600';
      case 'Baixa':
        return lida ? baseClass : 'bg-blue-50 border-l-4 border-blue-600';
      default:
        return baseClass;
    }
  }

  getTypeIcon(tipo: string): string {
    switch (tipo) {
      case 'Vacina':
        return '💉';
      case 'Vermifugo':
        return '💊';
      case 'Estoque':
        return '📦';
      case 'Higiene':
        return '🛁';
      case 'Veterinario':
        return '🏥';
      case 'Financeiro':
        return '💰';
      default:
        return '🔔';
    }
  }

  getUnreadCount(): number {
    return this.notifications.filter(n => !n.lida).length;
  }
}
