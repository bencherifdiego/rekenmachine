import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Rekenmachine } from './rekenmachine';

describe('Rekenmachine', () => {
  let component: Rekenmachine;
  let fixture: ComponentFixture<Rekenmachine>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Rekenmachine]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Rekenmachine);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
