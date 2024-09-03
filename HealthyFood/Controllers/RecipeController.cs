using AutoMapper;
using HealthyFood.Data;
using HealthyFood.Dto;
using HealthyFood.Interfaces;
using HealthyFood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HealthyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipe()
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetRecipes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int id)
        {
            if(!_recipeRepository.RecipeExist(id))
            {
                return NotFound();
            }
            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipe);
        }




        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRecipe([FromQuery] int catId, [FromBody] RecipeDto recipeCreate)
        {
            if (recipeCreate == null)
            {
                return BadRequest("Recipe data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);

            if (!_recipeRepository.Create(recipeMap, catId))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, "Internal server error");
            }

            return Ok("Successfully Created");
        }


        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRecipe(int recipeId, [FromQuery] int catId, [FromBody] RecipeDto updateRecipe)
        {
            if(updateRecipe == null)
                return BadRequest(ModelState);
            if(recipeId != updateRecipe.Id)
                return BadRequest(ModelState);
            if(!_recipeRepository.RecipeExist(recipeId))
                return NotFound();  
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipemap = _mapper.Map<Recipe>(updateRecipe);
            if(!_recipeRepository.Update(catId, recipemap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }

            return NoContent();
        }


        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExist(recipeId))
                return NotFound();
            var recipeToDelete = _recipeRepository.GetRecipe(recipeId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_recipeRepository.Delete(recipeToDelete))
                ModelState.AddModelError("", "Something went wrong while deleting");

            return Ok();
        }




       
    }
}
