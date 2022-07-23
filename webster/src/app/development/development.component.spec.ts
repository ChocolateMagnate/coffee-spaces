import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DevelopmentComponent } from './development.component';

describe('DevelopmentComponent', () => {
  let component: DevelopmentComponent;
  let fixture: ComponentFixture<DevelopmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DevelopmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DevelopmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
