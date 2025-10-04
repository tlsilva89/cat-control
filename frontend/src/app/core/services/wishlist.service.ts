import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Wishlist, CreateWishlistRequest } from '../models/wishlist.model';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private apiUrl = `${environment.apiUrl}/wishlist`;

  constructor(private http: HttpClient) {}

  getWishlist(): Observable<Wishlist[]> {
    return this.http.get<Wishlist[]>(this.apiUrl);
  }

  getWishlistByPriority(prioridade: string): Observable<Wishlist[]> {
    return this.http.get<Wishlist[]>(`${this.apiUrl}/priority/${prioridade}`);
  }

  getWishlistById(id: number): Observable<Wishlist> {
    return this.http.get<Wishlist>(`${this.apiUrl}/${id}`);
  }

  createWishlistItem(data: CreateWishlistRequest): Observable<Wishlist> {
    return this.http.post<Wishlist>(this.apiUrl, data);
  }

  updateWishlistItem(id: number, data: Partial<CreateWishlistRequest>): Observable<Wishlist> {
    return this.http.put<Wishlist>(`${this.apiUrl}/${id}`, data);
  }

  markAsPurchased(id: number): Observable<Wishlist> {
    return this.http.post<Wishlist>(`${this.apiUrl}/${id}/purchase`, {});
  }

  deleteWishlistItem(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
