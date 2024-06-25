using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for ScaleRecipePage.xaml
    /// </summary>
    public partial class ScaleRecipePage : Page
    {
        private RecipeApp recipeApp;

        /// <summary>
        /// Initializes a new instance of the ScaleRecipePage class.
        /// </summary>
        /// <param name="recipeApp">The RecipeApp instance to use for recipe operations.</param>
        public ScaleRecipePage(RecipeApp recipeApp)
        {
            InitializeComponent();
            this.recipeApp = recipeApp;
            LoadRecipes();
        }

        /// <summary>
        /// Loads the list of recipes into the RecipesListBox.
        /// </summary>
        private void LoadRecipes()
        {
            // Populate the RecipesListBox with recipe names
            RecipesListBox.ItemsSource = recipeApp.GetRecipes().Select(r => r.Name).ToList();
        }

        /// <summary>
        /// Handles the click event of the Scale Recipe button.
        /// </summary>
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

        /// <summary>
        /// Displays the details of a scaled recipe.
        /// </summary>
        /// <param name="recipe">The scaled recipe to display.</param>
        private void DisplayScaledRecipe(Recipe recipe)
        {
            string ingredients = string.Join("\n", recipe.Ingredients.Select(i => $"{i.Quantity} {i.Unit} of {i.Name}"));
            string steps = string.Join("\n", recipe.Steps);
            double totalCalories = recipe.CalculateTotalCalories();

            MessageBox.Show($"Scaled Recipe: {recipe.Name}\n\nIngredients:\n{ingredients}\n\nSteps:\n{steps}\n\nTotal Calories: {totalCalories:F2}",
                "Scaled Recipe Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Handles the selection changed event of the RecipesListBox.
        /// </summary>
        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
        }
    }
}