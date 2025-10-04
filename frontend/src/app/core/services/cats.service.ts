import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Cat, CreateCatRequest, UpdateCatRequest } from '../models/cat.model';

@Injectable({
  providedIn: 'root'
})
export class CatsService {
  private apiUrl = `${environment.apiUrl}/cats`;

  constructor(private http: HttpClient) {}

  getCats(): Observable<Cat[]> {
    return this.http.get<Cat[]>(this.apiUrl);
  }

  getCatById(id: number): Observable<Cat> {
    return this.http.get<Cat>(`${this.apiUrl}/${id}`);
  }

  createCat(data: CreateCatRequest): Observable<Cat> {
    return this.http.post<Cat>(this.apiUrl, data);
  }

  updateCat(id: number, data: UpdateCatRequest): Observable<Cat> {
    return this.http.put<Cat>(`${this.apiUrl}/${id}`, data);
  }

  deleteCat(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
