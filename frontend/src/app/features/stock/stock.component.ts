import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { StockService } from '../../core/services/stock.service';
import { Stock } from '../../core/models/stock.model';

@Component({
  selector: 'app-stock',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.scss']
})
export class StockComponent implements OnInit {
  stocks: Stock[] = [];
  loading = true;
  selectedCategory = 'all';
  
  categories = [
    { value: 'all', label: 'Todos' },
    { value: 'Ração', label: 'Ração' },
    { value: 'Petiscos', label: 'Petiscos' },
    { value: 'Sachês', label: 'Sachês' },
    { value: 'Areia', label: 'Areia Higiênica' },
    { value: 'Brinquedos', label: 'Brinquedos' },
    { value: 'Higiene', label: 'Higiene' }
  ];

  constructor(private stockService: StockService) {}

  ngOnInit(): void {
    this.loadStock();
  }

  loadStock(): void {
    this.loading = true;
    
    if (this.selectedCategory === 'all') {
      this.stockService.getStock().subscribe({
        next: (stocks) => {
          this.stocks = stocks;
          this.loading = false;
        }
      });
    } else {
      this.stockService.getStockByCategory(this.selectedCategory).subscribe({
        next: (stocks) => {
          this.stocks = stocks;
          this.loading = false;
        }
      });
    }
  }

  filterByCategory(category: string): void {
    this.selectedCategory = category;
    this.loadStock();
  }

  deleteStock(id: number): void {
    if (confirm('Tem certeza que deseja excluir este item?')) {
      this.stockService.deleteStock(id).subscribe({
        next: () => {
          this.loadStock();
        }
      });
    }
  }

  getStockStatusClass(stock: Stock): string {
    if (stock.alertaReposicao) return 'bg-red-100 text-red-800';
    if (stock.diasParaVencer && stock.diasParaVencer <= 7) return 'bg-yellow-100 text-yellow-800';
    return 'bg-green-100 text-green-800';
  }

  getStockStatusText(stock: Stock): string {
    if (stock.alertaReposicao) return 'Estoque baixo';
    if (stock.diasParaVencer && stock.diasParaVencer <= 0) return 'Vencido';
    if (stock.diasParaVencer && stock.diasParaVencer <= 7) return `Vence em ${stock.diasParaVencer} dias`;
    return 'OK';
  }
}
