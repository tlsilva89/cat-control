import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CatsService } from '../../core/services/cats.service';
import { Cat } from '../../core/models/cat.model';

@Component({
  selector: 'app-cats',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './cats.component.html',
  styleUrls: ['./cats.component.scss']
})
export class CatsComponent implements OnInit {
  cats: Cat[] = [];
  loading = true;

  constructor(private catsService: CatsService) {}

  ngOnInit(): void {
    this.loadCats();
  }

  loadCats(): void {
    this.loading = true;
    this.catsService.getCats().subscribe({
      next: (cats) => {
        this.cats = cats;
        this.loading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar gatos:', error);
        this.loading = false;
      }
    });
  }

  deleteCat(id: number): void {
    if (confirm('Tem certeza que deseja excluir este gato?')) {
      this.catsService.deleteCat(id).subscribe({
        next: () => {
          this.loadCats();
        },
        error: (error) => {
          console.error('Erro ao excluir gato:', error);
        }
      });
    }
  }
}
