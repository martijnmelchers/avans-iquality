import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuddygrouplistComponent } from './buddygrouplist.component';

describe('BuddygrouplistComponent', () => {
  let component: BuddygrouplistComponent;
  let fixture: ComponentFixture<BuddygrouplistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuddygrouplistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuddygrouplistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
