import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationService } from '@app/services';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { environment } from '@src/environments/environment';
import { Observable, of } from 'rxjs';
import { catchError, delay, map, switchMap, tap } from 'rxjs/operators';
import * as fromActions from './save.actions';
import { EstateCreateRequest, EstateResponse } from './save.models';

type Action = fromActions.All;

@Injectable()
export class SaveEffects {
  constructor(
    private actions: Actions,
    private httpClient: HttpClient,
    private router: Router,
    private notification: NotificationService
  ) { }

  create$: Observable<Action> = createEffect(() =>
    this.actions.pipe(
      ofType(fromActions.Types.CREATE),
      map((action: fromActions.Create) => action.estate),
      switchMap( (request: EstateCreateRequest) =>
        this.httpClient.post<EstateResponse>(`${environment.url}estate`, request)
        .pipe(
          delay(1000),
          tap((response: EstateResponse) => {
            this.router.navigate(['/estate/list']);
          }),
          map((estate: EstateResponse) => new fromActions.CreateSuccess(estate)),
          catchError(err => {
            this.notification.error(`Error creating estate: ${err.message}`);
            return of(new fromActions.CreateError(err.message));
          })
        )
      )
    )
  );

  read$: Observable<Action> = createEffect( () =>
    this.actions.pipe(
      ofType(fromActions.Types.READ),
      switchMap( () =>
        this.httpClient.get<EstateResponse[]>(`${environment.url}estate`)
        .pipe(
          // tap(response => console.log('API Response:', response)), // DEBUG
          delay(1000),
          map( (estates: EstateResponse[]) => new fromActions.ReadSuccess(estates) ),
          catchError(err => of(new fromActions.ReadError(err.message)))
        )
      )
    )
  );

}
