import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Notification, CreateNotificationRequest } from '../models/notification.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private apiUrl = `${environment.apiUrl}/notifications`;
  private unreadCountSubject = new BehaviorSubject<number>(0);
  public unreadCount$ = this.unreadCountSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadUnreadCount();
  }

  getNotifications(lida?: boolean): Observable<Notification[]> {
    let params = new HttpParams();
    if (lida !== undefined) {
      params = params.set('lida', lida.toString());
    }
    
    return this.http.get<Notification[]>(this.apiUrl, { params });
  }

  getUnreadNotifications(): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.apiUrl}/unread`);
  }

  getNotificationById(id: number): Observable<Notification> {
    return this.http.get<Notification>(`${this.apiUrl}/${id}`);
  }

  getUnreadCount(): Observable<{ count: number }> {
    return this.http.get<{ count: number }>(`${this.apiUrl}/unread/count`).pipe(
      tap(response => this.unreadCountSubject.next(response.count))
    );
  }

  createNotification(data: CreateNotificationRequest): Observable<Notification> {
    return this.http.post<Notification>(this.apiUrl, data);
  }

  markAsRead(id: number): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.apiUrl}/${id}/read`, {}).pipe(
      tap(() => this.loadUnreadCount())
    );
  }

  markAllAsRead(): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.apiUrl}/read-all`, {}).pipe(
      tap(() => this.unreadCountSubject.next(0))
    );
  }

  deleteNotification(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.loadUnreadCount())
    );
  }

  private loadUnreadCount(): void {
    this.getUnreadCount().subscribe();
  }
}
