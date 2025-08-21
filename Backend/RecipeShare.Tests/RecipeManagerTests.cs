using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using RecipeShare.API.Managers;
using RecipeShare.Database.Models;

namespace RecipeShare.Tests
{
	public class RecipeManagerTests : IClassFixture<GetRecipeDbFixture>
	{
		private readonly RecipeManager _manager;
		private readonly GetRecipeDbFixture _fixture;

		public RecipeManagerTests(GetRecipeDbFixture fixture)
		{
			_manager = new RecipeManager(fixture.Context);
			_fixture = fixture;
		}

		[Fact]
		public void GetRecipes()
		{
			var result = _manager.GetAllRecipes();
			var expected = _fixture.Context.Recipes.ToList();

			Assert.Equal(expected, result.Value!);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void GetRecipeById(int id)
		{
			var result = _manager.GetRecipeByID(id);
			var expected = _fixture.Context.Recipes.Single(r => r.Id == id);

			Assert.True(result.IsSuccess);
			result.Value!.Should().BeEquivalentTo(expected);
		}

		[Theory]
		[InlineData(99)]
		[InlineData(-1)]
		public void GetRecipeById_Fail(int id)
		{
			var result = _manager.GetRecipeByID(id);

			Assert.False(result.IsSuccess);
			Assert.Null(result.Value);
		}

		[Theory]
		[InlineData("Vegetarian", 2)]
		[InlineData("Vegan", 1)]
		[InlineData("Random", 0)]
		public void GetRecipeByDietaryTag(string tag, int expectedCount)
		{
			var result = _manager.GetRecipeByDietaryTag(tag);
			var expected = _fixture.Context.Recipes.ToList();

			Assert.Equal(expectedCount, result.Value!.Count);
		}

		[Fact]
		public void InsertRecipe_ShouldAddRecipe()
		{
			using var context = CreateInMemoryDbContext();
			var manager = new RecipeManager(context);

			var recipe = new Recipe
			{
				Title = "Pizza",
				Ingredients = new List<string> { "Dough", "Tomato Sauce", "Cheese" },
				Steps = new List<string> { "Prepare dough", "Spread sauce", "Add cheese", "Bake" },
				CookingTime = 30,
				DietaryTags = new List<string> { "Vegetarian" }
			};

			var result = manager.InsertRecipe(recipe);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);

			context.Recipes.Should().ContainSingle()
				.Which.Should().BeEquivalentTo(recipe, options => options.Excluding(r => r.Id));
		}

		[Fact]
		public void InsertRecipe_ShouldNotAddRecipe()
		{
			using var context = CreateInMemoryDbContext();
			var manager = new RecipeManager(context);

			var recipe = new Recipe
			{
				Title = "Pizza",
				Ingredients = new List<string>(),
				Steps = null!,
				CookingTime = 0,
				DietaryTags = new List<string> { "Vegetarian" }
			};

			var result = manager.InsertRecipe(recipe);
			Assert.False(result.IsSuccess);
		}

		[Fact]
		public void UpdateRecipe_ShouldModifyExistingRecipe()
		{
			using var context = CreateInMemoryDbContext();
			var manager = new RecipeManager(context);

			var recipe = new Recipe
			{
				Title = "Pizza",
				Ingredients = new List<string> { "Dough", "Tomato Sauce", "Cheese" },
				Steps = new List<string> { "Prepare dough", "Spread sauce", "Add cheese", "Bake" },
				CookingTime = 30,
				DietaryTags = new List<string> { "Vegetarian" }
			};

			var result = manager.InsertRecipe(recipe);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);

			recipe.Id = result.Value!.Id;
			recipe.CookingTime = 40;

			result = manager.UpdateRecipe(recipe);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);
			var updated = context.Recipes.Single();
			updated.CookingTime.Should().Be(40);
		}

		[Fact]
		public void UpdateRecipe_ShouldNotModifyExistingRecipe()
		{
			using var context = CreateInMemoryDbContext();
			var manager = new RecipeManager(context);

			var recipe = new Recipe
			{
				Title = "Pizza",
				Ingredients = new List<string> { "Dough", "Tomato Sauce", "Cheese" },
				Steps = new List<string> { "Prepare dough", "Spread sauce", "Add cheese", "Bake" },
				CookingTime = 30,
				DietaryTags = new List<string> { "Vegetarian" }
			};

			var result = manager.InsertRecipe(recipe);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);

			recipe.Id = 99;
			recipe.CookingTime = 0;
			recipe.Ingredients = null!;

			result = manager.UpdateRecipe(recipe);
			Assert.False(result.IsSuccess);
		}

		[Fact]
		public void DeleteRecipe_ShouldRemoveRecipe()
		{
			using var context = CreateInMemoryDbContext();
			var manager = new RecipeManager(context);

			var recipe = new Recipe
			{
				Title = "Pizza",
				Ingredients = new List<string> { "Dough", "Tomato Sauce", "Cheese" },
				Steps = new List<string> { "Prepare dough", "Spread sauce", "Add cheese", "Bake" },
				CookingTime = 30,
				DietaryTags = new List<string> { "Vegetarian" }
			};

			var result = manager.InsertRecipe(recipe);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);
			recipe.Id = result.Value!.Id;

			manager.DeleteRecipe(recipe.Id);

			context.Recipes.Should().BeEmpty();
		}

		[Fact]
		public void DeleteRecipe_ShouldNotRemoveRecipe()
		{
			using var context = CreateInMemoryDbContext();
			var manager = new RecipeManager(context);

			var recipe = new Recipe
			{
				Title = "Pizza",
				Ingredients = new List<string> { "Dough", "Tomato Sauce", "Cheese" },
				Steps = new List<string> { "Prepare dough", "Spread sauce", "Add cheese", "Bake" },
				CookingTime = 30,
				DietaryTags = new List<string> { "Vegetarian" }
			};

			var result = manager.InsertRecipe(recipe);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);
			recipe.Id = result.Value!.Id;

			manager.DeleteRecipe(99);
			
			Assert.Single(context.Recipes.ToList());
		}

		private SQLContext CreateInMemoryDbContext()
		{
			var options = new DbContextOptionsBuilder<SQLContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			return new SQLContext(options);
		}

	}
}