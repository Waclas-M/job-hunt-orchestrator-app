import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobOfferSection } from './job-offer-section';

describe('JobOfferSection', () => {
  let component: JobOfferSection;
  let fixture: ComponentFixture<JobOfferSection>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobOfferSection]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobOfferSection);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
