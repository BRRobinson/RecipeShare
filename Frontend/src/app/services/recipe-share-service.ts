import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Inject, Injectable } from '@angular/core';
import { API_URL } from '../app.config';
import { RecipeModel } from '../models/recipe.model'; // Adjust the path if your Recipe model is elsewhere

@Injectable({
  providedIn: 'root'
})
export class RecipeShareService {
  constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string){}

  getRecipes(): Observable<RecipeModel[]> {
    return this.http.get<RecipeModel[]>(`${this.apiUrl}/recipes`);
  }

  getRecipeById(id: number): Observable<RecipeModel> {
    return this.http.get<RecipeModel>(`${this.apiUrl}/recipes/GetRecipeByID/${id}`, );
  }
  
  insertRecipe(recipe: RecipeModel): Observable<RecipeModel> {
    return this.http.post<RecipeModel>(`${this.apiUrl}/recipes`, recipe);
  }

  updateRecipe(id: number, recipe: RecipeModel): Observable<RecipeModel> {
    return this.http.put<RecipeModel>(`${this.apiUrl}/recipes/${id}`, recipe);
  }

  deleteRecipe(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/recipes/${id}`);
  }
}
