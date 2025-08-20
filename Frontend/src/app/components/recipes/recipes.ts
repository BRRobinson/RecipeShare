import { Component, inject } from '@angular/core';
import { RecipeModel } from '../../models/recipe.model';
import { RecipeShareService } from '../../services/recipe-share-service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { RecipeDialog } from './recipe-dialog/recipe-dialog';
import { MatDivider } from '@angular/material/divider';
import { ReturnResult } from '../../models/return-result.model';
import { ConfirmDeleteDialog } from '../confirm-delete-dialog/confirm-delete-dialog';
import { ToastService } from '../../services/toast-service';

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatDivider],
  templateUrl: './recipes.html',
  styleUrl: './recipes.scss'
})
export class Recipes {

  constructor(
    private recipeService: RecipeShareService,
    private toastService: ToastService,
    private dialog: MatDialog
  ) {
    
  }
  recipes: RecipeModel[] = [];

  ngOnInit(): void {
    this.fetchRecipes();
  }

  fetchRecipes(): void {
    this.recipeService.getRecipes().subscribe({
      next: (result: ReturnResult<RecipeModel[]>) => {
        if (!result.isSuccess || !result.value) {
          this.toastService.show(`Failed to fetch recipes: ${result.message}`);
          console.error('Failed to fetch recipes:', result.message);
        } else {
          this.recipes = result.value;
        }
      },
      error: (err) => {
        this.toastService.show('Failed to fetch recipes');
        console.error('Failed to fetch recipes', err);
      }
    });
  }

  public insertRecipe(): void {
    const dialogRef = this.dialog.open(RecipeDialog, {
      width: '50vw',
      maxWidth: '75vw',
      height: '80vh',
      maxHeight: '95vh'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.recipes.push(result);
      }
    });
  }

  public editRecipe(recipeId: number): void {
    const recipe = this.recipes.find(r => r.id === recipeId);

    const dialogRef = this.dialog.open(RecipeDialog, {
      data: { ...recipe },
      width: '50vw',
      maxWidth: '75vw',
      height: '80vh',
      maxHeight: '95vh'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const index = this.recipes.findIndex(r => r.id === recipeId);
        this.recipes[index] = result;
      }
    });
  }

  public deleteRecipe(recipeId: number): void {
    const recipe = this.recipes.find(r => r.id === recipeId);

    const dialogRef = this.dialog.open(ConfirmDeleteDialog, {
      data: { title: 'Delete Recipe', message: `Are you sure you want to delete the recipe "${recipe!.title}"?` },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.recipeService.deleteRecipe(recipeId).subscribe({
          next: (result: ReturnResult<void>) => {
            if (!result.isSuccess) {
              this.toastService.show(`Failed to delete recipe: ${result.message}`);
              console.error('Failed to delete recipe:', result.message);
            } else {
              this.toastService.show('Succesfuly deleted recipe');
              console.error('Succesfuly deleted recipe');
              const index = this.recipes.findIndex(r => r.id === recipeId);
              if (index > -1) {
                this.recipes.splice(index, 1); 
              }
            }            
          },
          error: (err) => {
            this.toastService.show('Failed to delete recipe.');
            console.error('Failed to delete recipe.', err);
          }
        });
      }
    });

  }
}
