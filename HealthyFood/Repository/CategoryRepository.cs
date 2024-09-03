using HealthyFood.Data;
using HealthyFood.Interfaces;
using HealthyFood.Models;

namespace HealthyFood.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
           _context.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(i => i.Id).ToList(); 

        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(i => i.Id == id).FirstOrDefault();
        }

        public ICollection<Recipe> GetRecipeByCategory(int categoryId)
        {
           return _context.RecipeCategories.Where(e => e.CategoryId == categoryId).Select(p => p.Recipe).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
