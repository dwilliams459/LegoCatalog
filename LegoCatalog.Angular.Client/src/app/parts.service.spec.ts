/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PartsService } from './parts.service';

describe('Service: Parts', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PartsService]
    });
  });

  it('should ...', inject([PartsService], (service: PartsService) => {
    expect(service).toBeTruthy();
  }));
});
