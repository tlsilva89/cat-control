import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Finance, CreateFinanceRequest, FinanceSummary } from '../models/finance.model';

@Injectable({
  providedIn: 'root'
})
export class FinanceService {
  private apiUrl = `${environment.apiUrl}/finance`;

  constructor(private http: HttpClient) {}

  getFinances(): Observable<Finance[]> {
    return this.http.get<Finance[]>(this.apiUrl);
  }

  getFinancesByPeriod(startDate: Date, endDate: Date): Observable<Finance[]> {
    const params = new HttpParams()
      .set('startDate', startDate.toISOString())
      .set('endDate', endDate.toISOString());
    
    return this.http.get<Finance[]>(`${this.apiUrl}/period`, { params });
  }

  getFinancesByCategory(categoria: string): Observable<Finance[]> {
    return this.http.get<Finance[]>(`${this.apiUrl}/category/${categoria}`);
  }

  getFinanceById(id: number): Observable<Finance> {
    return this.http.get<Finance>(`${this.apiUrl}/${id}`);
  }

  getFinanceSummary(year?: number, month?: number): Observable<FinanceSummary> {
    let params = new HttpParams();
    if (year) params = params.set('year', year.toString());
    if (month) params = params.set('month', month.toString());
    
    return this.http.get<FinanceSummary>(`${this.apiUrl}/summary`, { params });
  }

  createFinance(data: CreateFinanceRequest): Observable<Finance> {
    return this.http.post<Finance>(this.apiUrl, data);
  }

  updateFinance(id: number, data: Partial<CreateFinanceRequest>): Observable<Finance> {
    return this.http.put<Finance>(`${this.apiUrl}/${id}`, data);
  }

  deleteFinance(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
