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
        public async Task<IHttpActionResult> SearchRecipes(string search, int count)
        {
            try
            {
                var result = await Task.Run(() =>_recipeService.SelectPreviewRecipeModelBySearchText(search, count));
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> SearchRecipeModel(int id)
        {
            //1456
            try
            {
                var result = await Task.Run(() => _recipeService.SelectRecipeModelByRecipeId(id));
                return Ok(new RecipeModelById{Recipes = result});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
