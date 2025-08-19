export class RecipeModel {
    id: number;
    title: string;
    ingredients: string[];
    steps: string[];
    cookingTimeMinutes: number;
    dietaryTags?: string[]; // Optional field for dietary tags

    constructor(
        id: number,
        title: string,
        ingredients: string[],
        steps: string[],
        cookingTimeMinutes: number,
        dietaryTags?: string[]
    ) {
        this.id = id;
        this.title = title;
        this.ingredients = ingredients;
        this.steps = steps;
        this.cookingTimeMinutes = cookingTimeMinutes;
        this.dietaryTags = dietaryTags || [];
    }
}
