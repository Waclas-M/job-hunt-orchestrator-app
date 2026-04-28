import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CvConfigurationModal } from './cv-configuration-modal';

describe('CvConfigurationModal', () => {
  let component: CvConfigurationModal;
  let fixture: ComponentFixture<CvConfigurationModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CvConfigurationModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CvConfigurationModal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
