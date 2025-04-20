import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-estate-new',
  templateUrl: './estate-new.component.html',
  styleUrls: ['./estate-new.component.scss']
})
export class EstateNewComponent implements OnInit {
  loading$!: Observable<boolean | null>;
  photoLoaded!: string;

  constructor() { }

  ngOnInit(): void {
  }

  registerEstate(form: NgForm): void {

  }

  onFilesChanged(url: any): void {
    if (url) {
      this.photoLoaded = url
    }
  }

}
