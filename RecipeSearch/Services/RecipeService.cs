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
using RecipeSearch.Models.SearchRecipe;

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

        internal async Task<List<RecipePreviewModel>> SelectRecipePreviewModelBySearchText(SearchRecipeModel searchRecipeModel)
        {
            var foundRecipes = _recipeDao.SelectAlLRecipesBySearchText(searchRecipeModel.Search, searchRecipeModel.DishIds, searchRecipeModel.DishSubCategoryIds, searchRecipeModel.DishMainCategoryIds, searchRecipeModel.IngredientIds, searchRecipeModel.IngredientCategoryIds, searchRecipeModel.FeatureIds, searchRecipeModel.FeatureCategoryIds, searchRecipeModel.Citrus, searchRecipeModel.Nut, searchRecipeModel.Sugar, searchRecipeModel.Mushroom, searchRecipeModel.Gluten, searchRecipeModel.CowMilk, searchRecipeModel.Wheat, searchRecipeModel.Egg, searchRecipeModel.Vegetarian, searchRecipeModel.Count).ToList();
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
                    DishId = recipe.DishId,
                    DishSubCategoryId = recipe.DishSubCategoryId,
                    DishMainCategoryId = recipe.DishMainCategoryId,
                    IngredientIds = recipe.IngredientIds,
                    IngredientCategoryIds = recipe.IngredientCategoryIds,
                    FeatureIds = recipe.FeatureIds,
                    FeatureCategoryIds = recipe.FeatureCategoryIds
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