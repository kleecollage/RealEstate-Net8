import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { SpinnerModule } from '@app/shared/indicators';
import { EstateListRoutingModule } from './estate-list-routing.module';
import { EstateListComponent } from './estate-list.component';


@NgModule({
  declarations: [
    EstateListComponent
  ],
  imports: [
    CommonModule,
    EstateListRoutingModule,
    SpinnerModule,
    MatButtonModule,
    MatCardModule
  ]
})
export class EstateListModule { }
