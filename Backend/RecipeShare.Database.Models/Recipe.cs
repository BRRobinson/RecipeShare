using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RecipeShare.Database.Models
{
	public class Recipe
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required.")]
		public required string Title { get; set; }

		[Required(ErrorMessage = "Ingredients are required.")]
		public required List<string> Ingredients { get; set; }

		[Required(ErrorMessage = "Steps are required.")]
		public required List<string> Steps { get; set; }

		public int CookingTime { get; set; }

		[AllowNull]
		public List<string>? DietaryTags { get; set; }
	}
}
