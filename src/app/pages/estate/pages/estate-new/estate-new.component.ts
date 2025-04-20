import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import * as fromRoot from '@app/store';
import * as fromList from '../../store/save';

@Component({
  selector: 'app-estate-new',
  templateUrl: './estate-new.component.html',
  styleUrls: ['./estate-new.component.scss']
})
export class EstateNewComponent implements OnInit {
  loading$!: Observable<boolean | null>;
  photoLoaded!: string;

  constructor( private store: Store<fromRoot.State> ) { }

  ngOnInit(): void {
  }

  registerEstate(form: NgForm): void {
    if (form.valid) {
      this.loading$ = this.store.pipe(select(fromList.getLoading));
      const estateCreateRequest: fromList.EstateCreateRequest = {
        name: form.value.name,
        picture: this.photoLoaded,
        price: Number(form.value.price),
        address: form.value.address,
      }
      this.store.dispatch(new fromList.Create(estateCreateRequest))
    }
  }

  onFilesChanged(url: any): void {
    if (url) {
      this.photoLoaded = url
    }
  }

}
