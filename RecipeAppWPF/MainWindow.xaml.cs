using System.Windows;

namespace RecipeAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RecipeApp recipeApp;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            recipeApp = new RecipeApp();
        }

        /// <summary>
        /// Handles the click event of the Add Recipe button.
        /// Navigates to the AddRecipePage.
        /// </summary>
        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddRecipePage(recipeApp));
        }

        /// <summary>
        /// Handles the click event of the View Recipes button.
        /// Navigates to the ViewRecipesPage.
        /// </summary>
        private void ViewRecipes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ViewRecipesPage(recipeApp));
        }

        /// <summary>
        /// Handles the click event of the Scale Recipe button.
        /// Navigates to the ScaleRecipePage.
        /// </summary>
        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ScaleRecipePage(recipeApp));
        }

        /// <summary>
        /// Handles the click event of the Remove Recipe button.
        /// Navigates to the RemoveRecipePage.
        /// </summary>
        private void RemoveRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RemoveRecipePage(recipeApp));
        }

        /// <summary>
        /// Handles the click event of the Edit Recipe button.
        /// Navigates to the EditRecipePage.
        /// </summary>
        private void EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EditRecipePage(recipeApp));
        }

        /// <summary>
        /// Handles the click event of the Filter Recipes button.
        /// Navigates to the FilterRecipesPage.
        /// </summary>
        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FilterRecipesPage(recipeApp));
        }

        /// <summary>
        /// Handles the click event of the Exit button.
        /// Shuts down the application.
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}