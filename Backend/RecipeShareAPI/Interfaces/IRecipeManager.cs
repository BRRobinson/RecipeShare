using RecipeShare.Database.Models;

namespace RecipeShare.API.Interfaces
{
	public interface IRecipeManager
	{
		public List<Recipe> GetAllRecipes();

		public Recipe GetRecipeByID(int id);

		public Recipe InsertRecipe(Recipe recipe);

		public Recipe UpdateRecipe(Recipe recipe);

		public void DeleteRecipe(int id);

	}
}
