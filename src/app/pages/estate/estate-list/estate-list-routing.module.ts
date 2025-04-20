import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EstateListComponent } from '@app/pages/estate/estate-list/estate-list.component';

const routes: Routes = [
  {
    path: '',
    component: EstateListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EstateListRoutingModule { }
