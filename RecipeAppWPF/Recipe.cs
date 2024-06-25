﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeAppWPF
{
    /// <summary>
    /// Represents a recipe in the application.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Gets or sets the name of the recipe.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of ingredients in the recipe.
        /// </summary>
        public List<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the list of steps in the recipe.
        /// </summary>
        public List<string> Steps { get; set; }

        private List<double> originalCalories;
        private List<string> originalQuantities;
        private bool calorieWarningShown = false;

        /// <summary>
        /// Initializes a new instance of the Recipe class.
        /// </summary>
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
            originalCalories = new List<double>();
            originalQuantities = new List<string>();
        }

        /// <summary>
        /// Adds an ingredient to the recipe.
        /// </summary>
        /// <param name="ingredient">The ingredient to add.</param>
        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            originalCalories.Add(ingredient.Calories);
            originalQuantities.Add($"{ingredient.Quantity} {ingredient.Unit}");
        }

        /// <summary>
        /// Scales the recipe by a given factor.
        /// </summary>
        /// <param name="scaleFactor">The factor to scale the recipe by.</param>
        /// <returns>True if scaling was successful, false otherwise.</returns>
        public bool ScaleRecipe(double scaleFactor)
        {
            // Check if the scale factor is valid
            if (scaleFactor != 0.5 && scaleFactor != 2 && scaleFactor != 3)
            {
                return false; // Invalid scale factor
            }

            // Scale ingredient quantities and calories
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Quantity *= scaleFactor;
                Ingredients[i].Calories *= scaleFactor;
            }
            return true;
        }

        /// <summary>
        /// Resets the ingredient quantities to their original values.
        /// </summary>
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

        /// <summary>
        /// Resets the ingredient calories to their original values.
        /// </summary>
        public void ResetCalories()
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Calories = originalCalories[i];
            }
        }

        /// <summary>
        /// Displays the recipe details in a message box.
        /// </summary>
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

        /// <summary>
        /// Gets a summary of the recipe.
        /// </summary>
        /// <returns>A string containing a summary of the recipe.</returns>
        public string GetSummary()
        {
            return $"{Name} | Ingredients: {Ingredients.Count} | Steps: {Steps.Count} | Calories: {CalculateTotalCalories():F0}";
        }

        /// <summary>
        /// Calculates the total calories of the recipe.
        /// </summary>
        /// <returns>The total calories of the recipe.</returns>
        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(i => i.Calories);
        }

        /// <summary>
        /// Notifies the user if the total calories exceed 300.
        /// </summary>
        public void NotifyIfCaloriesExceedLimit()
        {
            double totalCalories = CalculateTotalCalories();
            if (totalCalories > 300 && !calorieWarningShown)
            {
                MessageBox.Show($"Warning: Total calories of {Name} exceed 300!", "Calorie Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                calorieWarningShown = true;
            }
        }

        /// <summary>
        /// Resets the calorie warning flag.
        /// </summary>
        public void ResetCalorieWarning()
        {
            calorieWarningShown = false;
        }

        /// <summary>
        /// Clears all data from the recipe.
        /// </summary>
        public void ClearRecipe()
        {
            Ingredients.Clear();
            Steps.Clear();
            originalQuantities.Clear();
            originalCalories.Clear();
        }
    }
}