import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { Recipes } from './components/recipes/recipes';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: Home },
    { path: 'recipes', component: Recipes },
];
