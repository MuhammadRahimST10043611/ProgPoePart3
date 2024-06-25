using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    public partial class ScaleRecipePage : Page
    {
        private RecipeApp recipeApp;

        public ScaleRecipePage(RecipeApp recipeApp)
        {
            InitializeComponent();
            this.recipeApp = recipeApp;
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            RecipesListBox.ItemsSource = recipeApp.GetRecipes().Select(r => r.Name).ToList();
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListBox.SelectedItem != null && ScaleFactorComboBox.SelectedItem != null)
            {
                string selectedRecipeName = RecipesListBox.SelectedItem.ToString();
                Recipe selectedRecipe = recipeApp.Recipes.FirstOrDefault(r => r.Name == selectedRecipeName);

                // Cast the selected item to double
                double scaleFactor = Convert.ToDouble((ScaleFactorComboBox.SelectedItem as ComboBoxItem).Content);

                if (selectedRecipe != null)
                {
                    if (selectedRecipe.ScaleRecipe(scaleFactor))
                    {
                        MessageBox.Show($"Recipe scaled successfully by a factor of {scaleFactor}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Display the scaled recipe
                        DisplayScaledRecipe(selectedRecipe);
                    }
                    else
                    {
                        MessageBox.Show("Failed to scale the recipe. Please use a valid scale factor (0.5, 2, or 3).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe and a scale factor.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisplayScaledRecipe(Recipe recipe)
        {
            string ingredients = string.Join("\n", recipe.Ingredients.Select(i => $"{i.Quantity} {i.Unit} of {i.Name}"));
            string steps = string.Join("\n", recipe.Steps);
            double totalCalories = recipe.CalculateTotalCalories();

            MessageBox.Show($"Scaled Recipe: {recipe.Name}\n\nIngredients:\n{ingredients}\n\nSteps:\n{steps}\n\nTotal Calories: {totalCalories:F2}",
                "Scaled Recipe Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
        }
    }
}