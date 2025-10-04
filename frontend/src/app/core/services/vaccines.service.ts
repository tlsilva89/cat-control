import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Vaccine, CreateVaccineRequest, UpdateVaccineRequest } from '../models/vaccine.model';

@Injectable({
  providedIn: 'root'
})
export class VaccinesService {
  private apiUrl = `${environment.apiUrl}/vaccines`;

  constructor(private http: HttpClient) {}

  getVaccines(): Observable<Vaccine[]> {
    return this.http.get<Vaccine[]>(this.apiUrl);
  }

  getCatVaccines(catId: number): Observable<Vaccine[]> {
    return this.http.get<Vaccine[]>(`${this.apiUrl}/cat/${catId}`);
  }

  getVaccineById(id: number): Observable<Vaccine> {
    return this.http.get<Vaccine>(`${this.apiUrl}/${id}`);
  }

  getUpcomingVaccines(days: number = 30): Observable<Vaccine[]> {
    return this.http.get<Vaccine[]>(`${this.apiUrl}/upcoming?days=${days}`);
  }

  createVaccine(data: CreateVaccineRequest): Observable<Vaccine> {
    return this.http.post<Vaccine>(this.apiUrl, data);
  }

  updateVaccine(id: number, data: UpdateVaccineRequest): Observable<Vaccine> {
    return this.http.put<Vaccine>(`${this.apiUrl}/${id}`, data);
  }

  deleteVaccine(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
