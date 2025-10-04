import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { VaccinesService } from '../../core/services/vaccines.service';
import { CatsService } from '../../core/services/cats.service';
import { Vaccine } from '../../core/models/vaccine.model';
import { Cat } from '../../core/models/cat.model';

@Component({
  selector: 'app-vaccines',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './vaccines.component.html',
  styles: []
})
export class VaccinesComponent implements OnInit {
  vaccines: Vaccine[] = [];
  cats: Cat[] = [];
  loading = true;
  selectedFilter = 'all';

  constructor(
    private vaccinesService: VaccinesService,
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

    this.loadVaccines();
  }

  loadVaccines(): void {
    if (this.selectedFilter === 'upcoming') {
      this.vaccinesService.getUpcomingVaccines(30).subscribe({
        next: (vaccines) => {
          this.vaccines = vaccines;
          this.loading = false;
        }
      });
    } else {
      this.vaccinesService.getVaccines().subscribe({
        next: (vaccines) => {
          this.vaccines = vaccines;
          this.loading = false;
        }
      });
    }
  }

  filterVaccines(filter: string): void {
    this.selectedFilter = filter;
    this.loadVaccines();
  }

  deleteVaccine(id: number): void {
    if (confirm('Tem certeza que deseja excluir esta vacina?')) {
      this.vaccinesService.deleteVaccine(id).subscribe({
        next: () => {
          this.loadVaccines();
        }
      });
    }
  }

  getVaccineStatusClass(vaccine: Vaccine): string {
    if (!vaccine.diasParaProxima) return '';
    if (vaccine.diasParaProxima < 0) return 'bg-red-100 text-red-800';
    if (vaccine.diasParaProxima <= 7) return 'bg-yellow-100 text-yellow-800';
    return 'bg-green-100 text-green-800';
  }

  getVaccineStatusText(vaccine: Vaccine): string {
    if (!vaccine.diasParaProxima) return 'Sem próxima dose';
    if (vaccine.diasParaProxima < 0) return 'Atrasada';
    if (vaccine.diasParaProxima === 0) return 'Hoje';
    if (vaccine.diasParaProxima <= 7) return `Em ${vaccine.diasParaProxima} dias`;
    return `Em ${vaccine.diasParaProxima} dias`;
  }
}
