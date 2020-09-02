import { TestBed } from '@angular/core/testing';

import { LyricProcessorService } from './lyric-processor.service';

describe('LyricProcessorService', () => {
  let service: LyricProcessorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LyricProcessorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
