using HealthyFood.Data;
using HealthyFood.Dto;
using HealthyFood.Interfaces;
using HealthyFood.Migrations;
using HealthyFood.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthyFood.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;
        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Recipe recipe, int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category == null)
                return false;

            // Kullanıcıyı tarif ile ilişkilendir

            var recipeCategory = new RecipeCategory
            {
                Recipe = recipe,
                Category = category
            };

            _context.Recipes.Add(recipe);
            _context.RecipeCategories.Add(recipeCategory);

            return Save();
        }

        public bool Delete(Recipe recipe)
        {
            _context.Remove(recipe);
            return Save();  
        }

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.Where(i => i.Id == id).FirstOrDefault();
        }

        public Recipe GetRecipeByTitle(string title)
        {
            return _context.Recipes.Where(i => i.Title == title).FirstOrDefault();
        }

        public ICollection<Recipe> GetRecipes()
        {
            return _context.Recipes.OrderBy(i => i.Id).ToList();
        }

       

        public Recipe GetRecipeTrimToUpper(RecipeDto recipeDto)
        {
            return GetRecipes().Where(c => c.Title.Trim().ToUpper() == recipeDto.Title.TrimEnd().ToUpper()).FirstOrDefault();
        }

        public bool RecipeExist(int recipeId)
        {
            return _context.Recipes.Any(i => i.Id == recipeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(int categoryId, Recipe recipe)
        {
           _context.Update(recipe);
            return Save();
        }
    }
}



