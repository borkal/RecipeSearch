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
using System.Web;
using Newtonsoft.Json;

namespace RecipeSearch.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RecipeController : ApiController
    {
        private RecipeService.RecipeService _recipeService;
        private RecipeDao _recipeDao;

        public RecipeController()
        {
            _recipeService = new RecipeService.RecipeService();
            _recipeDao = new RecipeDao();
        }

        private int randomRecipeId()
        {
            Random r = new Random();
            int id = r.Next(1429, 2239);

            return id;
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
        [Route("recipe/searchRecipesPaged")]
        public async Task<IHttpActionResult> SearchRecipesPaged(string search, int pageNumber, int pageSize,
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
                var searchRecipeModel = new SearchRecipeModel2
                {
                    Search = search,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
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

                var result = await _recipeService.SelectRecipePreviewModelBySearchTextPaged(searchRecipeModel);

                int pageSize2 = searchRecipeModel.PageSize;
                int pageNumber2 = searchRecipeModel.PageNumber;
                int totalCount = result.Count();
                int pageAmount = (int)Math.Ceiling(totalCount / (double)pageSize);

                bool hasPreviousPage = pageNumber == 1 ? false : true;
                bool hasNextPage = pageAmount - pageNumber > 0 ? true : false;

                var resultRecipesAfterPaging = result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                result = resultRecipesAfterPaging;

                var metadata = new
                {
                    TotalCount = totalCount,
                    PageCount = result.Count,
                    PageSize = pageSize2,
                    PageNumber = pageNumber2,
                    PagesAmount = pageAmount,
                    NextPage = hasNextPage,
                    PrevPage = hasPreviousPage
                };

                if (pageNumber > pageAmount)
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, $"The page number {pageNumber} for search `{search}` does not exist !"));
                }

                else
                {
                    HttpContext.Current.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(new
                    {
                        TotalCount = totalCount,
                        PageCount = result.Count,
                        PageSize = pageSize2,
                        PageNumber = pageNumber2,
                        PagesAmount = pageAmount,
                        NextPage = hasNextPage,
                        PrevPage = hasPreviousPage, //pytanie czy tutaj czy w headerze te dane?
                        Recipes = result
                    });
                }
            }
            catch (Exception e)
            {
                //return InternalServerError(e);
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No results found for `{search}` !"));
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
        public async Task<IHttpActionResult> SearchRandomRecipe(int count)
        {
            try
            {
                var result = await _recipeService.GetRandomRecipes(count);
                
                return Ok(new
                {
                    recipes = result
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("recipe/searchDayRecipe")]
        public async Task<IHttpActionResult> SearchDayRecipe()
        {

            var lastDayRowInDatabase = _recipeDao.SelectRecipeOfTheDayRowFromDatabase();
            int id;

            if (lastDayRowInDatabase.DayRecipeDate == System.DateTime.Now.ToString("yyyy-MM-dd"))
            {
                id = lastDayRowInDatabase.DayRecipeRecipeId;
            }
            else
            {
                id = randomRecipeId();
                string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                _recipeDao.InsertRecipeOfTheDayRowToDatabase(date, id);
            }

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

        [HttpPost]
        [Route("recipe/insertRecipeRate")]
        public async Task<IHttpActionResult> InsertRecipeRate(int recipeId, int rate, string username)
        {
            try
            {
                var check = _recipeDao.SelectUserRateDataFromRateTable(recipeId, username);

                if (check.recipeId !=0 && check.userName != null)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, $"User {username} already rated recipe with id {recipeId}! as {check.recipeRate} !"));
                }
                else
                {
                    await _recipeService.InsertRecipeRateIntoDatabase(recipeId, rate, username);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("recipe/searchUserRecipeRate")]
        public async Task<IHttpActionResult> searchUserRecipeRate(int id, string username)
        {
            try
            {
                var result = await _recipeService.SelectUserRateDataFromRateTable(id, username);
                return Ok(new
                {
                    recipeRate = result
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}