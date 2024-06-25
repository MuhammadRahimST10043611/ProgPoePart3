using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for FilterRecipesPage.xaml
    /// </summary>
    public partial class FilterRecipesPage : Page
    {
        private RecipeApp recipeApp;

        public FilterRecipesPage(RecipeApp recipeApp)
        {
            InitializeComponent();
            this.recipeApp = recipeApp;
        }

        /// <summary>
        /// Filters recipes based on the input ingredient
        /// </summary>
        private void FilterRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredientName = IngredientFilterTextBox.Text.ToLower();
            var filteredRecipes = recipeApp.Recipes
                .Where(r => r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientName)))
                .Select(r => r.Name)
                .ToList();

            if (filteredRecipes.Any())
            {
                FilteredRecipesListBox.ItemsSource = filteredRecipes;
            }
            else
            {
                MessageBox.Show($"No recipes found containing '{ingredientName}'.", "No Results", MessageBoxButton.OK, MessageBoxImage.Information);
                FilteredRecipesListBox.ItemsSource = null;
            }
        }

        /// <summary>
        /// Displays details of the selected recipe
        /// </summary>
        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilteredRecipesListBox.SelectedItem != null)
            {
                string selectedRecipeName = FilteredRecipesListBox.SelectedItem.ToString();
                Recipe selectedRecipe = recipeApp.Recipes.FirstOrDefault(r => r.Name == selectedRecipeName);
                if (selectedRecipe != null)
                {
                    selectedRecipe.DisplayRecipe();
                }
            }
        }

        private void FilteredRecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // This method is currently empty
        }
    }
}