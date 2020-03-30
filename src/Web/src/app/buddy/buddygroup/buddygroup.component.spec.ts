import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuddygroupComponent } from './buddygroup.component';

describe('BuddygroupComponent', () => {
  let component: BuddygroupComponent;
  let fixture: ComponentFixture<BuddygroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuddygroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuddygroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
