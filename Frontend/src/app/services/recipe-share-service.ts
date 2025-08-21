import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Inject, Injectable } from '@angular/core';
import { API_URL } from '../app.config';
import { RecipeModel } from '../models/recipe.model'; // Adjust the path if your Recipe model is elsewhere
import { ReturnResult } from '../models/return-result.model';

@Injectable({
  providedIn: 'root'
})
export class RecipeShareService {
  constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string){}

  getRecipes(): Observable<ReturnResult<RecipeModel[]>> {
    return this.http.get<ReturnResult<RecipeModel[]>>(`${this.apiUrl}/recipes`);
  }

  getRecipeById(id: number): Observable<ReturnResult<RecipeModel>> {
    return this.http.get<ReturnResult<RecipeModel>>(`${this.apiUrl}/recipes/GetRecipeByID/${id}`, );
  }
  
  getRecipeByDietaryTags(dietaryTag: string): Observable<ReturnResult<RecipeModel[]>> {
    return this.http.get<ReturnResult<RecipeModel[]>>(`${this.apiUrl}/recipes/GetRecipesByDietaryTags/${dietaryTag}`, );
  }
  
  insertRecipe(recipe: RecipeModel): Observable<ReturnResult<RecipeModel>> {
    return this.http.post<ReturnResult<RecipeModel>>(`${this.apiUrl}/recipes`, recipe);
  }

  updateRecipe(recipe: RecipeModel): Observable<ReturnResult<RecipeModel>> {
    return this.http.put<ReturnResult<RecipeModel>>(`${this.apiUrl}/recipes`, recipe);
  }

  deleteRecipe(id: number): Observable<ReturnResult<void>> {
    return this.http.delete<ReturnResult<void>>(`${this.apiUrl}/recipes/${id}`);
  }
}
