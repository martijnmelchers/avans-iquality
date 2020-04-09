import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuddyinviteComponent } from './buddyinvite.component';

describe('BuddyinviteComponent', () => {
  let component: BuddyinviteComponent;
  let fixture: ComponentFixture<BuddyinviteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuddyinviteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuddyinviteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
