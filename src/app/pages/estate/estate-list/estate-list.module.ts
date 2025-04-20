import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EstateListRoutingModule } from './estate-list-routing.module';
import { EstateListComponent } from './estate-list.component';


@NgModule({
  declarations: [
    EstateListComponent
  ],
  imports: [
    CommonModule,
    EstateListRoutingModule
  ]
})
export class EstateListModule { }
