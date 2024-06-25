using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    public partial class RemoveRecipePage : Page
    {
        private RecipeApp recipeApp;

        public RemoveRecipePage(RecipeApp recipeApp)
        {
            InitializeComponent();
            this.recipeApp = recipeApp;
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            RecipesListBox.ItemsSource = recipeApp.GetRecipes().Select(r => r.Name).ToList();
        }

        private void RemoveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListBox.SelectedItem != null)
            {
                string selectedRecipeName = RecipesListBox.SelectedItem.ToString();
                Recipe selectedRecipe = recipeApp.Recipes.FirstOrDefault(r => r.Name == selectedRecipeName);
                if (selectedRecipe != null)
                {
                    recipeApp.RemoveRecipe(selectedRecipe);
                    LoadRecipes(); // Refresh the list
                    MessageBox.Show("Recipe removed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
