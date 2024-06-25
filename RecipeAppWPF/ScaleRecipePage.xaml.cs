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
            InitializeScaleFactors();
        }

        private void LoadRecipes()
        {
            RecipesListBox.ItemsSource = recipeApp.GetRecipes().Select(r => r.Name).ToList();
        }

        private void InitializeScaleFactors()
        {
            ScaleFactorComboBox.Items.Add(0.5);
            ScaleFactorComboBox.Items.Add(2.0);
            ScaleFactorComboBox.Items.Add(3.0);
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListBox.SelectedItem != null && ScaleFactorComboBox.SelectedItem != null)
            {
                string selectedRecipeName = RecipesListBox.SelectedItem.ToString();
                Recipe selectedRecipe = recipeApp.Recipes.FirstOrDefault(r => r.Name == selectedRecipeName);

                // Cast the selected item to double
                double scaleFactor = Convert.ToDouble(ScaleFactorComboBox.SelectedItem);

                if (selectedRecipe != null)
                {
                    if (selectedRecipe.ScaleRecipe(scaleFactor))
                    {
                        MessageBox.Show($"Recipe scaled successfully by a factor of {scaleFactor}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to scale the recipe. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe and a scale factor.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
        }
    }
}
