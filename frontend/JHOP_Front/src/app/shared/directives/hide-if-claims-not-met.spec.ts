import { HideIfClaimsNotMet } from './hide-if-claims-not-met';

describe('HideIfClaimsNotMet', () => {
  it('should create an instance', () => {
    const directive = new HideIfClaimsNotMet();
    expect(directive).toBeTruthy();
  });
});
