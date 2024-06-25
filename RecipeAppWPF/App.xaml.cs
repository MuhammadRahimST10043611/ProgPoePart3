using System.Collections.Generic;
using System.Linq;

namespace RecipeAppWPF
{
    public class RecipeApp
    {
        public List<Recipe> Recipes { get; private set; }

        public RecipeApp()
        {
            Recipes = new List<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        public List<Recipe> GetRecipes() => Recipes.OrderBy(r => r.Name).ToList();

        public void RemoveRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
        }
    }
}