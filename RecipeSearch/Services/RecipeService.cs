using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.WebPages.Scope;
using Model.DataAccess;
using Model.Enums;
using Model.Utilities.Parsers;
using RecipeSearch.Models;

namespace RecipeSearch.RecipeService
{
    public class RecipeService
    {
        private IRecipeDao _recipeDao;
        private IIngredientDao _ingredientDao;

        public RecipeService() : this(new RecipeDao(), new IngredientDao())
        {

        }

        public RecipeService(RecipeDao recipeDao, IngredientDao ingredientDao)
        {
            _recipeDao = recipeDao;
            _ingredientDao = ingredientDao;
        }

        internal async Task<List<RecipePreviewModel>> SelectRecipePreviewModelBySearchText(string searchText, int count, int[] dishIds, int[] dishSubCategoryIds, int[] dishMainCategoryIds, int[] ingredientIds, int[] ingredientCategoryIds)
        {
            var foundRecipes = _recipeDao.SelectAlLRecipesBySearchText(searchText, dishIds, dishSubCategoryIds, dishMainCategoryIds, ingredientIds, ingredientCategoryIds).Take(count).ToList();
            var recipeList = new List<RecipePreviewModel>();

            foreach (var recipe in foundRecipes)
            {
                var recipeModel = new RecipePreviewModel()
                {
                    Id = recipe.RecipeId.ToString(),
                    Blog = recipe.BlogName,
                    Image_Url = recipe.RecipeImage,
                    Title = recipe.RecipeName,
                    Url = recipe.RecipeUrl,
                    DishIds = recipe.DishIds,
                    DishSubCategoryIds = recipe.DishSubCategoryIds,
                    DishMainCategoryIds = recipe.DishMainCategoryIds,
                    IngredientIds = recipe.IngredientIds,
                    IngredientCategoryIds = recipe.IngredientCategoryIds
                };

                recipeList.Add(recipeModel);
            }

            return recipeList;
        }

        internal async Task<RecipeModel> SelectRecipeModelByRecipeId(int recipeId)
        {
            var recipe = _recipeDao.SelectRecipeByRecipeId(recipeId);
            IParser blog = null;

            switch (recipe.BlogId)
            {
                case (int)Blogs.FantazjeKulinarneMagdyK:
                     blog = new FantazjeMagdyKParser(recipe.RecipeUrl);
                     break;
                case (int)Blogs.KwestiaSmaku:
                    blog = new KwestiaSmakuParser(recipe.RecipeUrl);
                    break;
                case (int)Blogs.MojeDietetyczneFanaberie:
                    blog = new MojeDietetyczneFanaberieParser(recipe.RecipeUrl);
                    break;
            }

            var recipeModel = new RecipeModel
            {
                Blog = recipe.BlogName,
                Blog_Url = recipe.Blog_Url,
                Description = blog.Process ? blog.GetDescription() : new List<string>(),
                Image_Url = recipe.RecipeImage,
                Ingredients = blog.Process ? blog.GetIngredients() : new List<string>(),
                Id = recipeId.ToString(),
                Source_Url = recipe.RecipeUrl,
                Title = recipe.RecipeName
            };

            return recipeModel;
        }
    }
}