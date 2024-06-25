// EditRecipePage.xaml.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
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

        private void LoadRecipes()
        {
            RecipesComboBox.ItemsSource = recipeApp.GetRecipes().Select(r => r.Name);
        }

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

                ClearIngredientInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding ingredient: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StepDescription.Text))
            {
                currentSteps.Add(StepDescription.Text);
                StepsListBox.Items.Add(StepDescription.Text);
                StepDescription.Clear();
            }
        }

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

        private void ClearForm()
        {
            RecipeName.Clear();
            IngredientsListBox.Items.Clear();
            StepsListBox.Items.Clear();
            currentIngredients.Clear();
            currentSteps.Clear();
            ClearIngredientInputs();
            StepDescription.Clear();
            RecipesComboBox.SelectedIndex = -1;
            selectedRecipe = null;
        }

        private void ClearIngredientInputs()
        {
            IngredientName.Clear();
            IngredientQuantity.Clear();
            IngredientUnit.SelectedIndex = -1;
            IngredientCalories.Clear();
            IngredientFoodGroup.SelectedIndex = -1;
        }
    }
}