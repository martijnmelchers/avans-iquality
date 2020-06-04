import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OverflowMenuComponent } from './overflow-menu.component';

describe('OverflowMenuComponent', () => {
  let component: OverflowMenuComponent;
  let fixture: ComponentFixture<OverflowMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OverflowMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OverflowMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
