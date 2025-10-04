import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { WishlistService } from '../../core/services/wishlist.service';
import { CatsService } from '../../core/services/cats.service';
import { Wishlist } from '../../core/models/wishlist.model';
import { Cat } from '../../core/models/cat.model';

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {
  wishlist: Wishlist[] = [];
  cats: Cat[] = [];
  loading = true;
  selectedFilter = 'all';

  constructor(
    private wishlistService: WishlistService,
    private catsService: CatsService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading = true;

    this.catsService.getCats().subscribe({
      next: (cats) => {
        this.cats = cats;
      }
    });

    this.loadWishlist();
  }

  loadWishlist(): void {
    if (this.selectedFilter === 'all') {
      this.wishlistService.getWishlist().subscribe({
        next: (wishlist) => {
          this.wishlist = wishlist;
          this.loading = false;
        }
      });
    } else {
      this.wishlistService.getWishlistByPriority(this.selectedFilter).subscribe({
        next: (wishlist) => {
          this.wishlist = wishlist;
          this.loading = false;
        }
      });
    }
  }

  filterByPriority(priority: string): void {
    this.selectedFilter = priority;
    this.loadWishlist();
  }

  markAsPurchased(id: number): void {
    this.wishlistService.markAsPurchased(id).subscribe({
      next: () => {
        this.loadWishlist();
      }
    });
  }

  deleteItem(id: number): void {
    if (confirm('Tem certeza que deseja excluir este item?')) {
      this.wishlistService.deleteWishlistItem(id).subscribe({
        next: () => {
          this.loadWishlist();
        }
      });
    }
  }

  getPriorityClass(prioridade: string): string {
    switch (prioridade) {
      case 'Alta':
        return 'bg-red-100 text-red-800';
      case 'Média':
        return 'bg-yellow-100 text-yellow-800';
      case 'Baixa':
        return 'bg-green-100 text-green-800';
      default:
        return 'bg-gray-100 text-gray-800';
    }
  }

  getTotalEstimado(): number {
    return this.wishlist
      .filter(item => !item.comprado && item.precoEstimado)
      .reduce((total, item) => total + (item.precoEstimado || 0), 0);
  }

  getCompradosCount(): number {
    return this.wishlist.filter(item => item.comprado).length;
  }

  getPendentesCount(): number {
    return this.wishlist.filter(item => !item.comprado).length;
  }
}
