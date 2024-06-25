using RecipeAppWPF;
using System.Windows;

public class Recipe
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<string> Steps { get; set; }
    private List<double> originalCalories;
    private List<string> originalQuantities;
    public delegate void CalorieWarningHandler(string message);
    public event CalorieWarningHandler CalorieWarning;

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
        Steps = new List<string>();
        originalCalories = new List<double>();
        originalQuantities = new List<string>();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        Ingredients.Add(ingredient);
        originalCalories.Add(ingredient.Calories);
        originalQuantities.Add($"{ingredient.Quantity} {ingredient.Unit}");
    }

    public bool ScaleRecipe(double scaleFactor)
    {
        if (scaleFactor != 0.5 && scaleFactor != 2 && scaleFactor != 3)
        {
            return false; // Invalid scale factor
        }
        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients[i].Quantity *= scaleFactor;
            Ingredients[i].Calories *= scaleFactor;
        }
        return true;
    }

    public void ResetQuantities()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            string[] parts = originalQuantities[i].Split(' ');
            if (double.TryParse(parts[0], out double quantity))
            {
                Ingredients[i].Quantity = quantity;
            }
        }
    }

    public void ResetCalories()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients[i].Calories = originalCalories[i];
        }
    }

    public void DisplayRecipe()
    {
        string ingredients = string.Join("\n", Ingredients.Select(i => $"{i.Quantity} {i.Unit} of {i.Name}"));
        string steps = string.Join("\n", Steps);
        double totalCalories = CalculateTotalCalories();

        string calorieExplanation = totalCalories <= 100 ?
            "This recipe is low in calories, making it a healthy choice." :
            totalCalories <= 300 ?
            "This recipe is moderate in calories, suitable for balanced meals." :
            "This recipe is high in calories. It's best enjoyed in moderation.";

        MessageBox.Show($"Recipe: {Name}\n\nIngredients:\n{ingredients}\n\nSteps:\n{steps}\n\nTotal Calories: {totalCalories}\n\n{calorieExplanation}",
            "Recipe Details", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public string GetSummary()
    {
        return $"{Name} | Ingredients: {Ingredients.Count} | Steps: {Steps.Count} | Calories: {CalculateTotalCalories():F0}";
    }

    public double CalculateTotalCalories()
    {
        return Ingredients.Sum(i => i.Calories);
    }

    public void NotifyIfCaloriesExceedLimit()
    {
        double totalCalories = CalculateTotalCalories();
        if (totalCalories > 300)
        {
            CalorieWarning?.Invoke($"Warning: Total calories of {Name} exceed 300!");
        }
    }

    public void ClearRecipe()
    {
        Ingredients.Clear();
        Steps.Clear();
        originalQuantities.Clear();
        originalCalories.Clear();
    }
}