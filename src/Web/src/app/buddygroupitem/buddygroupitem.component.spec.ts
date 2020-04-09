import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuddygroupitemComponent } from './buddygroupitem.component';

describe('BuddygroupitemComponent', () => {
  let component: BuddygroupitemComponent;
  let fixture: ComponentFixture<BuddygroupitemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuddygroupitemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuddygroupitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
