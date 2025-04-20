import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.scss']
})
export class NotFoundComponent implements OnInit {

  image404: string = "https://firebasestorage.googleapis.com/v0/b/estates-app-d314b.firebasestorage.app/o/image%2F1745180065960_elite5.jpg?alt=media&token=b1a40c93-353d-4e1b-ab22-f4afde75581c";
  constructor() { }

  ngOnInit(): void {
  }

}
