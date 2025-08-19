import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeDialog } from './recipe-dialog';

describe('RecipeDialog', () => {
  let component: RecipeDialog;
  let fixture: ComponentFixture<RecipeDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecipeDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecipeDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
