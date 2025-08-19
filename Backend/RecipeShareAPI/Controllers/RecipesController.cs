using Microsoft.AspNetCore.Mvc;
using RecipeShare.API.Interfaces;
using RecipeShare.Database.Models;
using System.Reflection.Metadata.Ecma335;

namespace RecipeShareAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
		private readonly IRecipeManager _recipeManager;

		public RecipesController(ILogger<RecipesController> logger, IRecipeManager recipeManager)
        {
            _logger = logger;
			_recipeManager = recipeManager;
		}

		// GET /recipes
		[HttpGet]
		public IActionResult GetAllRecipes()
		{
			try
			{
				return Ok(_recipeManager.GetAllRecipes());
			}
			catch (Exception e)
			{
				_logger.LogError(e, "An error occurred while fetching the recipes.");
				return StatusCode(500, "Internal server error while fetching the recipes.");
			}
		}

		// GET /recipes/GetRecipeById/{id}
		[HttpGet("GetRecipeByID/{id}")]
		public IActionResult GetRecipeByID(int id)
		{
			try
			{
				return Ok(_recipeManager.GetRecipeByID(id));
			}
			catch (Exception e)
			{
				_logger.LogError(e, "An error occurred while fetching the recipe.");
				return StatusCode(500, "Internal server error while fetching the recipe.");
			}
		}

		// POST /recipes
		[HttpPost]
		public IActionResult InsertRecipe([FromBody] Recipe recipe)
		{
			try
			{
				return Ok(_recipeManager.InsertRecipe(recipe));
			}
			catch (Exception e)
			{
				_logger.LogError(e, "An error occurred while inserting the recipe.");
				return StatusCode(500, "Internal server error while inserting the recipe.");
			}
		}

		// PUT /recipes/{id}
		[HttpPut("{id}")]
		public IActionResult UpdateRecipe(int id, [FromBody] Recipe recipe)
		{
			try
			{
				return Ok(_recipeManager.UpdateRecipe(recipe));
			}
			catch (Exception e)
			{
				_logger.LogError(e, "An error occurred while updating the recipe.");
				return StatusCode(500, "Internal server error while updating the recipe.");
			}
		}

		// DELETE /recipes/{id}
		[HttpDelete("{id}")]
		public IActionResult DeleteRecipe(int id)
		{
			try
			{
				_recipeManager.DeleteRecipe(id);
				return Ok();
			}
			catch (Exception e)
			{
				_logger.LogError(e, "An error occurred while deleting the recipe.");
				return StatusCode(500, "Internal server error while deleting the recipe.");
			}
		}
	}
}
