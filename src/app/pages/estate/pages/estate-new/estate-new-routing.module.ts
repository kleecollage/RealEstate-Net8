import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EstateNewComponent } from '@app/pages/estate/pages/estate-new/estate-new.component';

const routes: Routes = [
  {
    path: '',
    component: EstateNewComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EstateNewRoutingModule { }
