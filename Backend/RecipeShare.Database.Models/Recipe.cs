using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RecipeShare.Database.Models
{
	public class Recipe
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Recipe title is required.")]
		public required string Title { get; set; }

		[Required(ErrorMessage = "Ingredients are required.")]
		public required List<string> Ingredients { get; set; }

		[Required(ErrorMessage = "Steps are required.")]
		public required List<string> Steps { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Cooking time must be at least 1 minute.")]
		public int CookingTime { get; set; }

		[AllowNull]
		public List<string> DietaryTags { get; set; }
	}
}
