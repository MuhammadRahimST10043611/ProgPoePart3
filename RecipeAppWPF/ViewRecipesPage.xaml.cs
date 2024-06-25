using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for ViewRecipesPage.xaml
    /// </summary>
    public partial class ViewRecipesPage : Page
    {
        private RecipeApp recipeApp;
        private Recipe currentlyDisplayedRecipe;

        /// <summary>
        /// Initializes a new instance of the ViewRecipesPage class.
        /// </summary>
        /// <param name="recipeApp">The RecipeApp instance to use for recipe operations.</param>
        public ViewRecipesPage(RecipeApp recipeApp)
        {
            InitializeComponent();
            this.recipeApp = recipeApp;
            LoadRecipes();
        }

        /// <summary>
        /// Loads the list of recipes into the RecipesListView.
        /// </summary>
        private void LoadRecipes()
        {
            // Create a list of anonymous objects with recipe details
            var recipes = recipeApp.GetRecipes().Select(r => new
            {
                r.Name,
                r.Ingredients,
                r.Steps,
                Calories = r.CalculateTotalCalories()
            }).ToList();

            RecipesListView.ItemsSource = recipes;
        }

        /// <summary>
        /// Handles the click event of the View Details button.
        /// </summary>
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

        /// <summary>
        /// Displays the details of a recipe.
        /// </summary>
        /// <param name="recipe">The recipe to display.</param>
        private void DisplayRecipeDetails(Recipe recipe)
        {
            recipe.ResetCalorieWarning(); // Reset the warning flag

            // Populate recipe details
            RecipeNameTextBlock.Text = recipe.Name;
            IngredientsTextBlock.Text = string.Join("\n", recipe.Ingredients.Select(i => $"• {i.Quantity} {i.Unit} of {i.Name}"));
            StepsItemsControl.ItemsSource = recipe.Steps;

            // Calculate and display total calories
            double totalCalories = recipe.CalculateTotalCalories();
            TotalCaloriesTextBlock.Text = $"Total Calories: {totalCalories:F0}";

            // Set calorie explanation based on total calories
            CalorieExplanationTextBlock.Text = totalCalories <= 100 ?
                "This recipe is low in calories, making it a healthy choice." :
                totalCalories <= 300 ?
                "This recipe is moderate in calories, suitable for balanced meals." :
                "This recipe is high in calories. It's best enjoyed in moderation.";

            RecipeDetailsPanel.Visibility = Visibility.Visible;

            // Trigger the calorie notification
            recipe.NotifyIfCaloriesExceedLimit();
        }

        /// <summary>
        /// Handles the selection changed event of the RecipesListView.
        /// </summary>
        private void RecipesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecipeDetailsPanel.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Handles the checked event of the step CheckBox.
        /// </summary>
        private void StepCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                // Disable and reduce opacity of checked steps
                checkBox.IsEnabled = false;
                checkBox.Opacity = 0.5;
            }
        }
    }
}