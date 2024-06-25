using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for EditRecipePage.xaml
    /// </summary>
    public partial class EditRecipePage : Page
    {
        private RecipeApp recipeApp;
        private Recipe selectedRecipe;
        private List<Ingredient> currentIngredients = new List<Ingredient>();
        private List<string> currentSteps = new List<string>();

        public EditRecipePage(RecipeApp recipeApp)
        {
            InitializeComponent();
            this.recipeApp = recipeApp;
            LoadRecipes();
        }

        /// <summary>
        /// Loads all recipes into the ComboBox
        /// </summary>
        private void LoadRecipes()
        {
            RecipesComboBox.ItemsSource = recipeApp.GetRecipes().Select(r => r.Name);
        }

        /// <summary>
        /// Loads the details of the selected recipe
        /// </summary>
        private void RecipesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipesComboBox.SelectedItem != null)
            {
                string selectedRecipeName = RecipesComboBox.SelectedItem.ToString();
                selectedRecipe = recipeApp.Recipes.FirstOrDefault(r => r.Name == selectedRecipeName);
                if (selectedRecipe != null)
                {
                    LoadRecipeDetails();
                }
            }
        }

        /// <summary>
        /// Populates the form with the selected recipe's details
        /// </summary>
        private void LoadRecipeDetails()
        {
            RecipeName.Text = selectedRecipe.Name;

            currentIngredients = new List<Ingredient>(selectedRecipe.Ingredients);
            IngredientsListBox.Items.Clear();
            foreach (var ingredient in currentIngredients)
            {
                IngredientsListBox.Items.Add($"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}");
            }

            currentSteps = new List<string>(selectedRecipe.Steps);
            StepsListBox.Items.Clear();
            foreach (var step in currentSteps)
            {
                StepsListBox.Items.Add(step);
            }

            ClearIngredientInputs();
            StepDescription.Clear();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementation similar to AddRecipePage
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementation similar to AddRecipePage
        }

        /// <summary>
        /// Saves the changes made to the recipe
        /// </summary>
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecipe != null)
            {
                try
                {
                    selectedRecipe.Name = RecipeName.Text;
                    selectedRecipe.Ingredients = new List<Ingredient>(currentIngredients);
                    selectedRecipe.Steps = new List<string>(currentSteps);

                    MessageBox.Show("Recipe updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadRecipes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Deletes the selected recipe
        /// </summary>
        private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecipe != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this recipe?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    recipeApp.RemoveRecipe(selectedRecipe);
                    MessageBox.Show("Recipe deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    LoadRecipes();
                }
            }
        }

        private void ClearFormButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Clears all input fields and resets the form
        /// </summary>
        private void ClearForm()
        {
            // Implementation similar to AddRecipePage
        }

        private void ClearIngredientInputs()
        {
            // Implementation similar to AddRecipePage
        }
    }
}