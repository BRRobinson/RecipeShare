using Microsoft.AspNetCore.Mvc;
using RecipeShare.API.Interfaces;
using RecipeShare.Database.Models;
using System.Reflection.Metadata.Ecma335;

namespace RecipeShare.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HealthController : ControllerBase
	{

		public HealthController()
		{
		}

		// GET /recipes
		[HttpGet("Status")]
		public IActionResult Status()
		{
			return Ok();
		}
	}
}
