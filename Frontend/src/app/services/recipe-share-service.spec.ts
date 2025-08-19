import { TestBed } from '@angular/core/testing';

import { RecipeShareService } from './recipe-share-service';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('RecipeService', () => {
  let service: RecipeShareService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RecipeShareService, provideHttpClientTesting()],
    });
    service = TestBed.inject(RecipeShareService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
