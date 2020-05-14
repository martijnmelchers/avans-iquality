import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTipComponent } from './addtip.component';

describe('AddTipComponent', () => {
  let component: AddTipComponent;
  let fixture: ComponentFixture<AddTipComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddTipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
