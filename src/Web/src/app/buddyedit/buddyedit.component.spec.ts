import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuddyeditComponent } from './buddyedit.component';

describe('BuddyeditComponent', () => {
  let component: BuddyeditComponent;
  let fixture: ComponentFixture<BuddyeditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuddyeditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuddyeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
