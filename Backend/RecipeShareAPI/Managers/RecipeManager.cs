using Microsoft.EntityFrameworkCore;
using RecipeShare.API.Interfaces;
using RecipeShare.Database;
using RecipeShare.Database.Models;

namespace RecipeShare.API.Managers
{
	public class RecipeManager : IRecipeManager
	{
		private readonly SQLContext _sqlContext;

		public RecipeManager(SQLContext dbContext)
		{
			_sqlContext = dbContext;
		}

		public ReturnResult<List<Recipe>> GetAllRecipes()
		{
			var recipes = _sqlContext.Set<Recipe>().ToList();

			return ReturnResult<List<Recipe>>.Success(recipes, "Recipes retrieved successfully.");
		}

		public ReturnResult<List<Recipe>> GetRecipeByDietaryTag(string dietaryTag)
		{
			var recipes = _sqlContext.Set<Recipe>().Where(r => r.DietaryTags.Contains(dietaryTag)).ToList();

			return ReturnResult<List<Recipe>>.Success(recipes, "Recipes retrieved successfully.");
		}
		
		public ReturnResult<Recipe> GetRecipeByID(int id)
		{
			var recipe = _sqlContext.Set<Recipe>().Find(id);
			if (recipe == null)
				return ReturnResult<Recipe>.Failed(default!, $"Recipe with ID {id} not found.");

			return ReturnResult<Recipe>.Success(recipe);
		}

		public ReturnResult<Recipe> InsertRecipe(Recipe recipe)
		{
			var validateResult = ValidateRecipe(recipe);

			if (!validateResult.IsSuccess)
				return validateResult;

			if (_sqlContext.Set<Recipe>().Any(r => r.Title == recipe.Title))
				return ReturnResult<Recipe>.Failed(default!, $"Recipe with Title {recipe.Title} already exists.");

			_sqlContext.Set<Recipe>().Add(recipe);
			_sqlContext.SaveChanges();

			return ReturnResult<Recipe>.Success(recipe, "Recipe inserted successfully.");
		}

		public ReturnResult<Recipe> UpdateRecipe(Recipe recipe)
		{
			var validateResult = ValidateRecipe(recipe);

			if (!validateResult.IsSuccess)
				return validateResult;

			var existingRecipeResult = GetRecipeByID(recipe.Id);
			if (!existingRecipeResult.IsSuccess)
				return existingRecipeResult;

			_sqlContext.Set<Recipe>().Update(recipe);
			_sqlContext.SaveChanges();

			return ReturnResult<Recipe>.Success(recipe, "Recipe updated successfully.");
		}

		public ReturnResult DeleteRecipe(int id)
		{
			var recipe = _sqlContext.Set<Recipe>().Find(id);
			if (recipe == null)
				return ReturnResult<Recipe>.Failed(default!, $"Recipe with ID {id} not found.");

			_sqlContext.Set<Recipe>().Remove(recipe);
			_sqlContext.SaveChanges();

			return ReturnResult.Success("Recipe deleted successfully.");
		}

		public ReturnResult<Recipe> ValidateRecipe(Recipe recipe)
		{
			if (recipe == null)
				return ReturnResult<Recipe>.Failed(default!, "Recipe cannot be null.");

			if (string.IsNullOrWhiteSpace(recipe.Title))
				return ReturnResult<Recipe>.Failed(recipe, "Title is required.");

			if (recipe.Ingredients == null || recipe.Ingredients.Count == 0)
				return ReturnResult<Recipe>.Failed(recipe, "Ingredients are required.");

			if (recipe.Steps == null || recipe.Steps.Count == 0)
				return ReturnResult<Recipe>.Failed(recipe, "Steps are required.");

			if (recipe.CookingTime < 1)
				return ReturnResult<Recipe>.Failed(recipe, "Cooking time must be at least 1 minute.");

			return ReturnResult<Recipe>.Success(recipe, "Recipe is valid.");
		}
	}
}
