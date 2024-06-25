using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            recipe.CalorieWarning += DisplayCalorieWarning;
            Recipes.Add(recipe);
        }

        public List<Recipe> GetRecipes() => Recipes.OrderBy(r => r.Name).ToList();

        public void RemoveRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
        }

        private void DisplayCalorieWarning(string message)
        {
            MessageBox.Show(message, "Calorie Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
