using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeShare.API.Interfaces;
using RecipeShare.Database.Models;
using System.Reflection.Metadata.Ecma335;

namespace RecipeShare.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DatabaseController : ControllerBase
	{
		private readonly SQLContext _dbContext;
		public DatabaseController(SQLContext dbContext)
		{
			_dbContext = dbContext;
		}

		public SQLContext DbContext { get; }

		// GET /recipes
		[HttpGet("DBSync")]
		public IActionResult DBSync()
		{
			_dbContext.Database.Migrate();
			return Ok();
		}
	}
}
