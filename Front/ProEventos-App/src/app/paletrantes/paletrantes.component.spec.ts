import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaletrantesComponent } from './paletrantes.component';

describe('PaletrantesComponent', () => {
  let component: PaletrantesComponent;
  let fixture: ComponentFixture<PaletrantesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaletrantesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaletrantesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
