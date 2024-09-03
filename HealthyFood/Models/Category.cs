﻿namespace HealthyFood.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RecipeCategory> RecipeCategories { get; set; }

    }
}
