import { Component, Inject } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { RecipeShareService } from '../../../services/recipe-share-service';
import { MatCard, MatCardModule } from '@angular/material/card';
import { MatDivider } from '@angular/material/divider';
import { MatIcon } from '@angular/material/icon';
import { RecipeModel } from '../../../models/recipe.model';
import { ReturnResult } from '../../../models/return-result.model';
import { ToastService } from '../../../services/toast-service';

@Component({
  selector: 'app-recipe-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatDivider,
    MatIcon
  ],
  templateUrl: './recipe-dialog.html',
  styleUrl: './recipe-dialog.scss'
})
export class RecipeDialog {

  public isEdit: boolean = false;

  public recipeForm: FormGroup;

  constructor(
    private recipeService: RecipeShareService,
    private toastService: ToastService,
    public dialogRef: MatDialogRef<RecipeDialog>,
    @Inject(MAT_DIALOG_DATA) public data: RecipeModel
  ) {
    if (this.data) {
      this.isEdit = true;
    }

    this.recipeForm = new FormGroup({
      
      title: new FormControl(this.data?.title || '', [
        Validators.required
      ]),
      ingredients: new FormArray(
        (this.data?.ingredients || []).map(i => new FormControl(i, Validators.required))),
      steps: new FormArray(
        (this.data?.steps || []).map(i => new FormControl(i, Validators.required))),
      cookingTime: new FormControl(this.data?.cookingTime || 1, [
        Validators.required,
        Validators.min(1)
      ]),
      dietaryTags: new FormArray(
        (this.data?.dietaryTags || []).map(i => new FormControl(i))) // optional
    });
  }
  
  get ingredients() {
    return this.recipeForm.get('ingredients') as FormArray;
  }

  get steps() {
    return this.recipeForm.get('steps') as FormArray;
  }

  get dietaryTags() {
    return this.recipeForm.get('dietaryTags') as FormArray;
  }

  addIngredient(value: string = '') {
    this.ingredients.push(new FormControl(value, Validators.required));
  }

  removeIngredient(index: number) {
    this.ingredients.removeAt(index);
  }

  addStep(value: string = '') {
    this.steps.push(new FormControl(value, Validators.required));
  }

  removeStep(index: number) {
    this.steps.removeAt(index);
  }

  addDietaryTag(value: string = '') {
    this.dietaryTags.push(new FormControl(value));
  }

  removeDietaryTag(index: number) {
    this.dietaryTags.removeAt(index);
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    if (this.recipeForm.invalid) {
      this.toastService.show('Please fill in all required fields.');
      return;
    }

    const recipe: RecipeModel = {
      id: this.data?.id || 0,
      title: this.recipeForm.value.title,
      ingredients: this.recipeForm.value.ingredients,
      steps: this.recipeForm.value.steps,
      cookingTime: this.recipeForm.value.cookingTime,
      dietaryTags: this.recipeForm.value.dietaryTags || []
    }
    if (!this.isEdit) {
      this.recipeService.insertRecipe(recipe).subscribe({
        next: (result: ReturnResult<RecipeModel>) => {
          if (!result.isSuccess) {
            this.toastService.show(`Failed to insert recipe: ${result.message}`);
            console.error('Failed to insert recipe:', result.message);
          } else if (!result.value) {
            this.toastService.show(`Succesfuly inserted recipe but return was empty: ${result.message}`);
            console.error('Succesfuly inserted recipe but return was empty:', result.message);
          } else {
            this.toastService.show('Succesfuly inserted recipe');
            console.error('Succesfuly inserted recipe');
            this.data = result.value;
            this.dialogRef.close(this.data);
          }
        },
        error: (err) => {
          this.toastService.show('Failed to insert recipe');
          console.error('Failed to insert recipe', err);
        }
      });
    } else {
      this.recipeService.updateRecipe(recipe).subscribe({
        next: (result: ReturnResult<RecipeModel>) => {
          if (!result.isSuccess) {
            this.toastService.show(`Failed to update recipes: ${result.message}`);
            console.error('Failed to update recipes:', result.message);
          } else if (!result.value) {
            this.toastService.show(`Succesfuly updated recipe but return was empty: ${result.message}`);
            console.error('Succesfuly updated recipe but return was empty:', result.message);
          } else {
            this.toastService.show('Succesfuly updated recipe');
            console.error('Succesfuly updated recipe');
            this.data = result.value;
            this.dialogRef.close(this.data);
          }
        },
        error: (err) => {
          this.toastService.show('Failed to update recipes');
          console.error('Failed to update recipes', err);
        }
      });
    }
  }
}
