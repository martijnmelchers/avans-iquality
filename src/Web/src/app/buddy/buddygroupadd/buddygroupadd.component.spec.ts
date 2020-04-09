import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuddygroupaddComponent } from './buddygroupadd.component';

describe('BuddygroupaddComponent', () => {
  let component: BuddygroupaddComponent;
  let fixture: ComponentFixture<BuddygroupaddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuddygroupaddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuddygroupaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
