using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for AddRecipePage.xaml
    /// </summary>
    public partial class AddRecipePage : Page
    {
        private RecipeApp recipeApp;
        private List<Ingredient> currentIngredients = new List<Ingredient>();
        private List<string> currentSteps = new List<string>();

        public AddRecipePage(RecipeApp recipeApp)
        {
            InitializeComponent();
            this.recipeApp = recipeApp;
        }

        /// <summary>
        /// Adds a new ingredient to the recipe
        /// </summary>
        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ingredient ingredient = new Ingredient
                {
                    Name = IngredientName.Text,
                    Quantity = double.Parse(IngredientQuantity.Text),
                    Unit = (IngredientUnit.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Calories = double.Parse(IngredientCalories.Text),
                    FoodGroup = (IngredientFoodGroup.SelectedItem as ComboBoxItem)?.Content.ToString()
                };

                currentIngredients.Add(ingredient);
                IngredientsListBox.Items.Add($"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}");

                // Clear ingredient input fields
                IngredientName.Clear();
                IngredientQuantity.Clear();
                IngredientUnit.SelectedIndex = -1;
                IngredientCalories.Clear();
                IngredientFoodGroup.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding ingredient: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Adds a new step to the recipe
        /// </summary>
        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StepDescription.Text))
            {
                currentSteps.Add(StepDescription.Text);
                StepsListBox.Items.Add(StepDescription.Text);
                StepDescription.Clear();
            }
        }

        /// <summary>
        /// Adds the complete recipe to the app
        /// </summary>
        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Recipe recipe = new Recipe
                {
                    Name = RecipeName.Text,
                    Ingredients = new List<Ingredient>(currentIngredients),
                    Steps = new List<string>(currentSteps)
                };

                recipeApp.AddRecipe(recipe);
                MessageBox.Show("Recipe added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            RecipeName.Clear();
            IngredientsListBox.Items.Clear();
            StepsListBox.Items.Clear();
            currentIngredients.Clear();
            currentSteps.Clear();
            IngredientName.Clear();
            IngredientQuantity.Clear();
            IngredientUnit.SelectedIndex = -1;
            IngredientCalories.Clear();
            IngredientFoodGroup.SelectedIndex = -1;
            StepDescription.Clear();
        }

        private void IngredientName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // This method is currently empty
        }
    }
}