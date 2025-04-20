import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SpinnerModule } from '@app/shared/indicators';
import { EntityPhotoModule } from '@app/shared/layouts';
import { PopupsModule } from '@app/shared/popups';
import { EstateNewRoutingModule } from './estate-new-routing.module';
import { EstateNewComponent } from './estate-new.component';


@NgModule({
  declarations: [
    EstateNewComponent
  ],
  imports: [
    CommonModule,
    EstateNewRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule,
    MatInputModule,
    MatCardModule,
    FlexLayoutModule,
    SpinnerModule,
    EntityPhotoModule,
    PopupsModule
  ]
})
export class EstateNewModule { }
