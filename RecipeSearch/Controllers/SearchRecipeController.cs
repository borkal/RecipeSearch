using System.Collections.Generic;
using System.Web.Mvc;
using Model.DataAccess;
using Model.Domain;

namespace RecipeSearch.Controllers
{
    public class SearchRecipeController : Controller
    {
        private readonly IIngredientDao _ingredientDao;
        // GET: SearchRecipe

        private readonly IRecipeDao _recipeDao;

        public SearchRecipeController()
        {
            _recipeDao = new RecipeDao();
            _ingredientDao = new IngredientDao();
        }

        public JsonResult SearchRecipesBasedOnSearchString(string searchString)
        {
            var recipeList = new List<Recipe>();

            var recipeIdsFound = _recipeDao.SearchRecipeIdsBasedOnSearchText(searchString);
            foreach (var recipe in recipeIdsFound)
            {
                var recipeToAdd = _recipeDao.SelectRecipeByRecipeId(recipe);
                recipeToAdd.RecipeIngredientsList = _ingredientDao.SelectAllIngredientByRecipeId(recipe);
                //recipeToAdd.RecipeDish = _dishDao.SelectDishByRecipeID(recipe);
            }

            return new JsonResult {Data = recipeList, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}