import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CatsService } from '../../core/services/cats.service';
import { VaccinesService } from '../../core/services/vaccines.service';
import { StockService } from '../../core/services/stock.service';
import { FinanceService } from '../../core/services/finance.service';
import { NotificationsService } from '../../core/services/notifications.service';
import { Cat } from '../../core/models/cat.model';
import { Vaccine } from '../../core/models/vaccine.model';
import { Stock } from '../../core/models/stock.model';
import { Notification } from '../../core/models/notification.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styles: []
})
export class DashboardComponent implements OnInit {
  cats: Cat[] = [];
  upcomingVaccines: Vaccine[] = [];
  lowStock: Stock[] = [];
  recentNotifications: Notification[] = [];
  totalGasto = 0;
  loading = true;

  stats = {
    totalCats: 0,
    upcomingVaccines: 0,
    lowStockItems: 0,
    monthlyExpense: 0
  };

  constructor(
    private catsService: CatsService,
    private vaccinesService: VaccinesService,
    private stockService: StockService,
    private financeService: FinanceService,
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading = true;

    this.catsService.getCats().subscribe({
      next: (cats) => {
        this.cats = cats;
        this.stats.totalCats = cats.length;
      }
    });

    this.vaccinesService.getUpcomingVaccines(30).subscribe({
      next: (vaccines) => {
        this.upcomingVaccines = vaccines;
        this.stats.upcomingVaccines = vaccines.length;
      }
    });

    this.stockService.getLowStock().subscribe({
      next: (stock) => {
        this.lowStock = stock;
        this.stats.lowStockItems = stock.length;
      }
    });

    this.financeService.getFinanceSummary().subscribe({
      next: (summary) => {
        this.stats.monthlyExpense = summary.gastoMensal;
        this.loading = false;
      }
    });

    this.notificationsService.getUnreadNotifications().subscribe({
      next: (notifications) => {
        this.recentNotifications = notifications.slice(0, 5);
      }
    });
  }
}
