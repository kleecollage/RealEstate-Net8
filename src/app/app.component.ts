import { Component, OnInit } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { NotificationService } from '@app/services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit{
  showSpinner = false;
  title = 'cli-inmueble-app';

  constructor(private fs:AngularFirestore, private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.fs.collection('test').stateChanges().subscribe(people => {
      console.log(people.map(person => person.payload.doc.data()));
    })
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

}
