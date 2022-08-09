import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitBugComponent } from './submit-bug.component';

describe('SubmitBugComponent', () => {
  let component: SubmitBugComponent;
  let fixture: ComponentFixture<SubmitBugComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmitBugComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubmitBugComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
