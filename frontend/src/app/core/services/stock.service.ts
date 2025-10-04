import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Stock, CreateStockRequest, UpdateStockRequest } from '../models/stock.model';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private apiUrl = `${environment.apiUrl}/stock`;

  constructor(private http: HttpClient) {}

  getStock(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.apiUrl);
  }

  getStockByCategory(categoria: string): Observable<Stock[]> {
    return this.http.get<Stock[]>(`${this.apiUrl}/category/${categoria}`);
  }

  getStockById(id: number): Observable<Stock> {
    return this.http.get<Stock>(`${this.apiUrl}/${id}`);
  }

  getLowStock(): Observable<Stock[]> {
    return this.http.get<Stock[]>(`${this.apiUrl}/low-stock`);
  }

  getExpiringStock(days: number = 30): Observable<Stock[]> {
    return this.http.get<Stock[]>(`${this.apiUrl}/expiring?days=${days}`);
  }

  createStock(data: CreateStockRequest): Observable<Stock> {
    return this.http.post<Stock>(this.apiUrl, data);
  }

  updateStock(id: number, data: UpdateStockRequest): Observable<Stock> {
    return this.http.put<Stock>(`${this.apiUrl}/${id}`, data);
  }

  deleteStock(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
