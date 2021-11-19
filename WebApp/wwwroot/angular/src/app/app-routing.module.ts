import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from 'src/category/category.component';
import { AppComponent } from './app.component';

const routes: Routes = [
 { path: '', redirectTo: '/angular', pathMatch: 'full'},
 { path: 'angular', component: AppComponent},
 { path: 'angular/category', component: CategoryComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
