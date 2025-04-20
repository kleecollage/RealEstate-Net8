import { Component, OnInit } from '@angular/core';
import * as fromRoot from '@app/store';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import * as fromList from '../../store/save';
import { EstateResponse } from '../../store/save';

@Component({
  selector: 'app-estate-list',
  templateUrl: './estate-list.component.html',
  styleUrls: ['./estate-list.component.scss']
})
export class EstateListComponent implements OnInit {
  estates$!: Observable<EstateResponse[] | null>;
  loading$!: Observable<boolean | null>;
  error$!: Observable<string | null>;
  pictureDefault: string = "https://firebasestorage.googleapis.com/v0/b/estates-app-d314b.firebasestorage.app/o/image%2F1745175230084_elite1.jpg?alt=media&token=246ef9a5-29a1-4f02-b05d-ad35b7d9c1f3"

  constructor(private store: Store<fromRoot.State>) { }

  ngOnInit(): void {
    console.log('Dispatching Read action');
    this.store.dispatch(new fromList.Read());
    this.loading$ = this.store.pipe(select(fromList.getLoading));
    this.estates$ = this.store.pipe(select(fromList.getEstates));
  }

}
