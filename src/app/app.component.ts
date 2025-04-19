import { Component, OnInit } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { Router } from '@angular/router';
import { NotificationService } from '@app/services';
import * as fromRoot from '@app/store';
import * as fromUser from '@app/store/user';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit{
  showSpinner = false;
  title = 'estates-app';

  user$!: Observable<fromUser.UserResponse>;
  isAuthorized$!: Observable<boolean>;

  constructor(
    private fs:AngularFirestore,
    private notificationService: NotificationService,
    private store: Store<fromRoot.State>,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.user$ = this.store.pipe(select(fromUser.getUser)) as Observable<fromUser.UserResponse>;
    this.isAuthorized$ = this.store.pipe(select(fromUser.getIsAuthorized)) as Observable<boolean>;
    this.store.dispatch(new fromUser.Init());
    /* TEST: Get data from firestore
    this.fs.collection('test').stateChanges().subscribe(people => {
      console.log(people.map(person => person.payload.doc.data()));
    }) */
  }

  onToggleSpinner(): void {
    this.showSpinner = !this.showSpinner
  }

  onFilesChanged(urls: string | string[]): void {
    console.log('urls', urls);
  }

  onSuccess(): void {
    this.notificationService.success('Process completed successfully');
  }

  onError(): void {
    this.notificationService.error('Process failed');
  }

  onSignOut(): void {
    localStorage.removeItem('token');
    this.store.dispatch(new fromUser.SignOut());
    this.router.navigate(['/auth/login']);
  }

}
