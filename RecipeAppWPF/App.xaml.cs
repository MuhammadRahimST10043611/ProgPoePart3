using System.Collections.Generic;
using System.Linq;

namespace RecipeAppWPF
{
    /// <summary>
    /// Represents the main application logic for managing recipes.
    /// </summary>
    public class RecipeApp
    {
        /// <summary>
        /// Gets the list of recipes in the application.
        /// </summary>
        public List<Recipe> Recipes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the RecipeApp class.
        /// </summary>
        public RecipeApp()
        {
            Recipes = new List<Recipe>();
        }

        /// <summary>
        /// Adds a new recipe to the application.
        /// </summary>
        /// <param name="recipe">The recipe to add.</param>
        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        /// <summary>
        /// Gets a sorted list of all recipes.
        /// </summary>
        /// <returns>A list of recipes sorted by name.</returns>
        public List<Recipe> GetRecipes() => Recipes.OrderBy(r => r.Name).ToList();

        /// <summary>
        /// Removes a recipe from the application.
        /// </summary>
        /// <param name="recipe">The recipe to remove.</param>
        public void RemoveRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
        }
    }
}