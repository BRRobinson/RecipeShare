using RecipeShare.Database.Models;

namespace RecipeShare.API.Interfaces
{
	public interface IRecipeManager
	{
		public ReturnResult<List<Recipe>> GetAllRecipes();

		public ReturnResult<List<Recipe>> GetRecipeByDietaryTag(string dietaryTag);

		public ReturnResult<Recipe> GetRecipeByID(int id);

		public ReturnResult<Recipe> InsertRecipe(Recipe recipe);

		public ReturnResult<Recipe> UpdateRecipe(Recipe recipe);

		public ReturnResult DeleteRecipe(int id);

	}
}
