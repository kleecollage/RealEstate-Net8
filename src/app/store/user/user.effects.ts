import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationService } from '@app/services';
import { UserResponse } from '@app/store/user/user.models';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { environment } from '@src/environments/environment';
import { Observable, of } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
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

  signInEmail$: Observable<Action> = createEffect(() =>
    this.actions.pipe(
      ofType(fromActions.Types.SIGN_IN_EMAIL),
      map((action: fromActions.SignInEmail) => action.credentials),
      switchMap( userData =>
        this.httpCient.post<UserResponse>(`${environment.url}user/login/`, userData)
        .pipe(
          tap((response: UserResponse) => {
            localStorage.setItem('token', response.token);
            this.router.navigate(['/']);
          }),
          map((response: UserResponse) => new fromActions.SignInEmailSuccess(response)),
          catchError(err => {
            this.notification.error("Credentials incorrect");
            return of(new fromActions.SignInEmailError(err.message));
          })
        )
      )
    )
  )

  init$: Observable<Action> = createEffect(() =>
    this.actions.pipe(
      ofType(fromActions.Types.INIT),
      switchMap( async () => localStorage.getItem('token')),
      switchMap( token => {
        if (token) {
          return this.httpCient.get<UserResponse>(`${environment.url}user`)
          .pipe(
            tap((response: UserResponse) => {
              console.log('user session data from server', response)
            }),
            map((response: UserResponse) => new fromActions.InitAuthorized(response.email, response || null)),
            catchError(err => {
              return of(new fromActions.InitError(err.message));
            })
          )
        } else {
          return of(new fromActions.InitUnauthorized());
        }
      })
    )
  )
}
