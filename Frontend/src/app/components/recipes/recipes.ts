import { Component, inject } from '@angular/core';
import { RecipeModel } from '../../models/recipe.model';
import { RecipeShareService } from '../../services/recipe-share-service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { RecipeDialog } from './recipe-dialog/recipe-dialog';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatDivider, RecipeDialog],
  templateUrl: './recipes.html',
  styleUrl: './recipes.scss'
})
export class Recipes {

  constructor(
    private recipeService: RecipeShareService,
    private dialog: MatDialog
  ) {
    
  }
  recipes: RecipeModel[] = [];

  ngOnInit(): void {
    this.fetchRecipes();
  }

  fetchRecipes(): void {
    this.recipeService.getRecipes().subscribe({
      next: (data) => {
        this.recipes = data;
      },
      error: (err) => {
        console.error('Failed to fetch recipes', err);
      }
    });
  }

  public insertRecipe(): void {
    const dialogRef = this.dialog.open(RecipeDialog, {});

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.recipes.push(result);
      }
    });
  }

  public editRecipe(recipeId: number): void {
    const recipe = this.recipes.find(r => r.id === recipeId);

    const dialogRef = this.dialog.open(RecipeDialog, {
      data: { ...recipe }
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
    this.recipeService.deleteRecipe(recipeId).subscribe({
      next: () => {
        this.fetchRecipes();
      },
      error: (err) => {
        console.error('Failed to fetch recipes', err);
      }
    });
  }
}
