import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FinanceService } from '../../core/services/finance.service';
import { Finance, FinanceSummary } from '../../core/models/finance.model';

@Component({
  selector: 'app-finance',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './finance.component.html',
  styleUrls: ['./finance.component.scss']
})
export class FinanceComponent implements OnInit {
  finances: Finance[] = [];
  summary!: FinanceSummary;
  loading = true;
  selectedMonth = new Date().getMonth() + 1;
  selectedYear = new Date().getFullYear();

  constructor(private financeService: FinanceService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading = true;

    this.financeService.getFinanceSummary(this.selectedYear, this.selectedMonth).subscribe({
      next: (summary) => {
        this.summary = summary;
        this.finances = summary.ultimosGastos;
        this.loading = false;
      }
    });
  }

  deleteFinance(id: number): void {
    if (confirm('Tem certeza que deseja excluir este registro?')) {
      this.financeService.deleteFinance(id).subscribe({
        next: () => {
          this.loadData();
        }
      });
    }
  }

  getCategoryEntries(): { category: string, amount: number }[] {
    return Object.entries(this.summary.gastoPorCategoria).map(([category, amount]) => ({
      category,
      amount
    }));
  }
}
