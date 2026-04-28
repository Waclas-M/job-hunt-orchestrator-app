import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LetterSection } from './letter-section';

describe('LetterSection', () => {
  let component: LetterSection;
  let fixture: ComponentFixture<LetterSection>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LetterSection]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LetterSection);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
