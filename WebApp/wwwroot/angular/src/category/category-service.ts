import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryDto } from 'src/types/categories';

@Injectable({ providedIn: 'root' })
export class CategoryService {
    private httpClient: HttpClient;

    constructor(private http: HttpClient) {
        this.httpClient = http;
    }

    public getCategories(): Observable<CategoryDto[]> {
        return this.httpClient.get<CategoryDto[]>("/api/categoriesApi");
    }
}
