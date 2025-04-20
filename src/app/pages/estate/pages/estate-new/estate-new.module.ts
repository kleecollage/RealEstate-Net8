import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EstateNewRoutingModule } from './estate-new-routing.module';
import { EstateNewComponent } from './estate-new.component';


@NgModule({
  declarations: [
    EstateNewComponent
  ],
  imports: [
    CommonModule,
    EstateNewRoutingModule
  ]
})
export class EstateNewModule { }
