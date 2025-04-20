import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EstateNewComponent } from './estate-new.component';

describe('EstateNewComponent', () => {
  let component: EstateNewComponent;
  let fixture: ComponentFixture<EstateNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EstateNewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EstateNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
