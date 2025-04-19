import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationService } from '@app/services';
import { UserResponse } from '@app/store/user/user.models';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { environment } from '@src/environments/environment';
import { catchError, map, Observable, of, switchMap, tap } from 'rxjs';
import * as fromActions from './user.actions';

type Action = fromActions.All;

@Injectable()
export class UserEffects {
  constructor(
    private httpCient: HttpClient,
    private actions: Actions,
    private notification: NotificationService,
    private router: Router
  ) {}

  signUpEmail$: Observable<Action> = createEffect(() =>
    this.actions.pipe(
      ofType(fromActions.Types.SIGN_UP_EMAIL),
      map((action: fromActions.SignUpEmail) => action.user),
      switchMap( userData =>
        this.httpCient.post<UserResponse>(`${environment.url}user/register/`, userData)
        .pipe(
          tap((response: UserResponse) => {
            localStorage.setItem('token', response.token);
            this.router.navigate(['/']);
          }),
          map((response: UserResponse) => new fromActions.SignUpEmailSuccess(response.email, response || null)),
          catchError(err => {
            this.notification.error("Error registering new user");
            return of(new fromActions.SignUpEmailError(err.message));
          })
        )
      )
    )
  )
}
