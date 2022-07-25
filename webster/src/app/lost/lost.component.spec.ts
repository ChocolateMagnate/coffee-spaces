import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LostComponent } from './lost.component';

describe('LostComponent', () => {
  let component: LostComponent;
  let fixture: ComponentFixture<LostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LostComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
