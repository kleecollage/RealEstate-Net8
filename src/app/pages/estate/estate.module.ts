import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { EstateRoutingModule } from './estate-routing.module';
import { effects, reducers } from './store';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    EstateRoutingModule,
    StoreModule.forFeature('estate', reducers),
    EffectsModule.forFeature(effects)
  ]
})
export class EstateModule { }
