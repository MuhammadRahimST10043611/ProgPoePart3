using System.Windows;

namespace RecipeAppWPF
{
    public partial class MainWindow : Window
    {
        private RecipeApp recipeApp;

        public MainWindow()
        {
            InitializeComponent();
            recipeApp = new RecipeApp();
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddRecipePage(recipeApp));
        }

        private void ViewRecipes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ViewRecipesPage(recipeApp));
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ScaleRecipePage(recipeApp));
        }

        private void RemoveRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RemoveRecipePage(recipeApp));
        }

        private void EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EditRecipePage(recipeApp));
        }

        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FilterRecipesPage(recipeApp));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}