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
            [FromUri] int[] ingredientCategoryIds,
            [FromUri] int[] featureIds,
            [FromUri] int[] featureCategoryIds,
            bool? citrus = null,
            bool? nut = null,
            bool? sugar = null,
            bool? mushroom = null,
            bool? gluten = null,
            bool? cowMilk = null,
            bool? wheat = null,
            bool? egg = null,
            bool? vegetarian = null
            )
        {
            try
            {
                var searchRecipeModel = new SearchRecipeModel
                {
                    Search = search,
                    Count = count,
                    DishIds = dishIds,
                    DishSubCategoryIds = dishSubCategoryIds,
                    DishMainCategoryIds = dishMainCategoryIds,
                    IngredientIds = ingredientIds,
                    IngredientCategoryIds = ingredientCategoryIds,
                    FeatureIds = featureIds,
                    FeatureCategoryIds = featureCategoryIds,
                    Citrus = citrus,
                    Nut = nut,
                    Sugar = sugar,
                    Mushroom = mushroom,
                    Gluten = gluten,
                    CowMilk = cowMilk,
                    Wheat = wheat,
                    Egg = egg,
                    Vegetarian = vegetarian
                };

                var result = await _recipeService.SelectRecipePreviewModelBySearchText(searchRecipeModel);              
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

        [HttpGet]
        [Route("recipe/searchRandomRecipe")]
        public async Task<IHttpActionResult> SearchRandomRecipe()
        {
            try
            {
                var result = await _recipeService.SelectRandomRecipeModel();
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
