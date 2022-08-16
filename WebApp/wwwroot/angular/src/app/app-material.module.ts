import { NgModule } from '@angular/core';
import { MatSliderModule } from '@angular/material/slider'
import {MatToolbarModule} from '@angular/material/toolbar';


@NgModule({
  imports: [
    MatSliderModule,
    MatToolbarModule,
  ],
  exports:[
    MatSliderModule,
    MatToolbarModule,
  ]
})

export class AppMaterialModule { }
