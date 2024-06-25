using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    public partial class ViewRecipesPage : Page
    {
        private RecipeApp recipeApp;
        private Recipe currentlyDisplayedRecipe;

        public ViewRecipesPage(RecipeApp recipeApp)
        {
            InitializeComponent();
            this.recipeApp = recipeApp;
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            var recipes = recipeApp.GetRecipes().Select(r => new
            {
                r.Name,
                r.Ingredients,
                r.Steps,
                Calories = r.CalculateTotalCalories()
            }).ToList();

            RecipesListView.ItemsSource = recipes;
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListView.SelectedItem != null)
            {
                var selectedRecipe = (dynamic)RecipesListView.SelectedItem;
                currentlyDisplayedRecipe = recipeApp.Recipes.FirstOrDefault(r => r.Name == selectedRecipe.Name);
                if (currentlyDisplayedRecipe != null)
                {
                    DisplayRecipeDetails(currentlyDisplayedRecipe);
                }
            }
        }

        private void DisplayRecipeDetails(Recipe recipe)
        {
            RecipeNameTextBlock.Text = recipe.Name;
            IngredientsTextBlock.Text = string.Join("\n", recipe.Ingredients.Select(i => $"• {i.Quantity} {i.Unit} of {i.Name}"));
            StepsItemsControl.ItemsSource = recipe.Steps;

            double totalCalories = recipe.CalculateTotalCalories();
            TotalCaloriesTextBlock.Text = $"Total Calories: {totalCalories:F0}";

            CalorieExplanationTextBlock.Text = totalCalories <= 100 ?
                "This recipe is low in calories, making it a healthy choice." :
                totalCalories <= 300 ?
                "This recipe is moderate in calories, suitable for balanced meals." :
                "This recipe is high in calories. It's best enjoyed in moderation.";

            RecipeDetailsPanel.Visibility = Visibility.Visible;
        }

        private void RecipesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecipeDetailsPanel.Visibility = Visibility.Collapsed;
        }

        private void StepCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                checkBox.IsEnabled = false;
                checkBox.Opacity = 0.5;
            }
        }
    }
}