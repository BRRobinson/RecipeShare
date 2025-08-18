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

		public List<Recipe> GetAllRecipes()
		{
			return _sqlContext.Set<Recipe>().ToList();
		}

		public Recipe GetRecipeByID(int id)
		{
			var recipe = _sqlContext.Set<Recipe>().Find(id);
			if (recipe == null)
			{
				throw new KeyNotFoundException($"Recipe with ID {id} not found.");
			}
			return recipe;
		}

		public Recipe InsertRecipe(Recipe recipe)
		{
			if (recipe == null)
			{
				throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
			}
			if (string.IsNullOrWhiteSpace(recipe.Title))
			{
				throw new ArgumentException("Recipe title is required.", nameof(Recipe.Title));
			}
			_sqlContext.Set<Recipe>().Add(recipe);
			_sqlContext.SaveChanges();
			return recipe;
		}

		public Recipe UpdateRecipe(Recipe recipe)
		{
			if (recipe == null)
			{
				throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
			}
			if (string.IsNullOrWhiteSpace(recipe.Title))
			{
				throw new ArgumentException("Recipe title is required.", nameof(Recipe.Title));
			}
			_sqlContext.Set<Recipe>().Update(recipe);
			_sqlContext.SaveChanges();
			return recipe;
		}

		public void DeleteRecipe(int id)
		{
			var recipe = _sqlContext.Set<Recipe>().Find(id);
			if (recipe == null)
			{
				throw new KeyNotFoundException($"Recipe with ID {id} not found.");
			}
			_sqlContext.Set<Recipe>().Remove(recipe);
			_sqlContext.SaveChanges();
		}

	}
}
