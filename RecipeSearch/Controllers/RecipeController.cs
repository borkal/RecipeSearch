using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Model.DataAccess;
using Model.Domain;
using Model.Utilities.Parsers;
using RecipeSearch.Models;
using RecipeSearch.Models.SearchRecipe;
using RecipeSearch.RecipeService;

namespace RecipeSearch.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RecipeController : ApiController
    {
        private RecipeService.RecipeService _recipeService;

        public RecipeController()
        {
            _recipeService = new RecipeService.RecipeService();
        }

        [HttpGet]
        public async Task<IHttpActionResult> SearchRecipes(string search, int count,
            [FromUri] int[] dishIds,
            [FromUri] int[] dishSubCategoryIds,
            [FromUri] int[] dishMainCategoryIds,
            [FromUri] int[] ingredientIds,
            [FromUri] int[] ingredientCategoryIds)
        {
            try
            {
                var result = await _recipeService.SelectRecipePreviewModelBySearchText(search, count, dishIds, dishSubCategoryIds, dishMainCategoryIds,ingredientIds,ingredientCategoryIds);              
                return Ok(new 
                {
                    count = result.Count,
                    recipes = result
                    
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> SearchRecipeModel(int id)
        {
            try
            {
                var result = await _recipeService.SelectRecipeModelByRecipeId(id);
                return Ok(new 
                {
                    recipe = result
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
