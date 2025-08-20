using Microsoft.EntityFrameworkCore;
using RecipeShare.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShare.Tests
{
	public class GetRecipeDbFixture : IDisposable
	{
		public SQLContext Context { get; private set; }

		public GetRecipeDbFixture()
		{
			var options = new DbContextOptionsBuilder<SQLContext>()
				.UseInMemoryDatabase(databaseName: "GetRecipesDb")
				.Options;

			Context = new SQLContext(options);

			// Seed data only once
			if (!Context.Recipes.Any())
			{
				Context.Recipes.AddRange(
				new Recipe
				{
					Id = 1,
					Title = "Salad",
					Ingredients = new List<string> { "Lettuce", "Tomato", "Cucumber" },
					Steps = new List<string> { "Wash the vegetables", "Chop the vegetables", "Mix them together" },
					CookingTime = 10,
					DietaryTags = new List<string> { "Vegan", "Gluten-Free" }
				},
				new Recipe
				{
					Id = 2,
					Title = "Pizza",
					Ingredients = new List<string> { "Dough", "Tomato Sauce", "Cheese" },
					Steps = new List<string> { "Prepare the dough", "Spread the sauce", "Add cheese", "Bake" },
					CookingTime = 30,
					DietaryTags = new List<string> { "Vegetarian" }
				},
				new Recipe
				{
					Id = 3,
					Title = "Hot Cross Buns",
					Ingredients = new List<string> { "Flour", "Sugar", "Spices", "Currants" },
					Steps = new List<string> { "Mix ingredients", "Knead dough", "Shape buns", "Bake" },
					CookingTime = 20,
					DietaryTags = new List<string> { "Vegetarian", "Nut-Free" }
				}
				);
				Context.SaveChanges();
			}
		}

		public void Dispose()
		{
			Context.Dispose();
		}
	}
}
