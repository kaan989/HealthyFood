using HealthyFood.Dto;
using HealthyFood.Models;

namespace HealthyFood.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        Recipe GetRecipeByTitle(string title);
        Recipe GetRecipeTrimToUpper(RecipeDto recipeDto);
        bool Create(Recipe recipe, int categoryId);
        bool Update(int categoryId, Recipe recipe);
       
        bool Delete(Recipe recipe);
        bool Save();
        bool RecipeExist(int recipeId);
    }
}
