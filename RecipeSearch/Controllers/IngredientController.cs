using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using RecipeSearch.Services;

namespace RecipeSearch.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IngredientController : ApiController
    {
        private readonly IngredientService _ingredientService;

        public IngredientController() : this(new IngredientService())
        {

        }

        public IngredientController(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [Route("ingredient/ingredients")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectIngredients()
        {
            try
            {
                var ingredientsList = await _ingredientService.SelectIngredients();
                return Ok(new
                {
                    ingredients = ingredientsList
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("ingredient/ingredientCategories")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectIngredientCategories()
        {
            try
            {
                var ingredientCategoriesList = await _ingredientService.SelectIngredientCategories();
                return Ok(new
                {
                    ingredientCategories = ingredientCategoriesList
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("ingredient/ingredientCategoriesXref")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectIngredientCategoriesXref()
        {
            try
            {
                var ingredietnCategoriesXrefList = await _ingredientService.SelectIngredientCategoryXref();
                return Ok(new
                {
                    ingredientCategoryXrefs = ingredietnCategoriesXrefList
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
