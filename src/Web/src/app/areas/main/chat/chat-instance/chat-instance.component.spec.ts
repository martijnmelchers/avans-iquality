import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatInstanceComponent } from './chat-instance.component';

describe('ChatInstanceComponent', () => {
  let component: ChatInstanceComponent;
  let fixture: ComponentFixture<ChatInstanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChatInstanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChatInstanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
