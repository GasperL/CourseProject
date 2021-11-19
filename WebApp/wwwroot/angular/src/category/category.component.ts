import { Component } from '@angular/core';
import { CategoryDto } from 'src/types/categories';
import { CategoryService } from './category-service';

@Component({
  selector: 'category-component',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.sass']
})

export class CategoryComponent {
  public categories: CategoryDto[] = [];

  constructor(private service: CategoryService) { }

  ngOnInit() {
    this.service.getCategories().subscribe(dto => {
      this.categories = dto;
    })
  }
}
