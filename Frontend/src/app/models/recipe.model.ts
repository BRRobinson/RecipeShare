export class RecipeModel {
    id: number;
    title: string;
    ingredients: string[];
    steps: string[];
    cookingTime: number;
    dietaryTags?: string[]; // Optional field for dietary tags

    constructor(
        id: number,
        title: string,
        ingredients: string[],
        steps: string[],
        cookingTime: number,
        dietaryTags?: string[]
    ) {
        this.id = id;
        this.title = title;
        this.ingredients = ingredients;
        this.steps = steps;
        this.cookingTime = cookingTime;
        this.dietaryTags = dietaryTags || [];
    }
}
