import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { DropZoneDirective } from './directives/drop-zone/drop-zone.directive';
import { FilesUploadComponent } from './files-upload.component';
import { FilesUploadDirective } from './files-upload.directive';


@NgModule({
  declarations: [
    FilesUploadComponent,
    FilesUploadDirective,
    DropZoneDirective
   ],
  imports: [
    CommonModule,
    MatDialogModule
  ],
  exports: [
    FilesUploadDirective
  ]
})
export class FilesUploadModule { }
