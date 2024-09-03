using Microsoft.AspNetCore.Identity;

namespace HealthyFood.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; }
    }
}
