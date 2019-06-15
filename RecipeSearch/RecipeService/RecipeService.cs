using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Model.DataAccess;
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
        
        public RecipePreviewModel SelectPreviewRecipeModelBySearchText(string searchText, int count)
        {
            var foundRecipes = _recipeDao.SelectAlLRecipesBySearchText(searchText).Take(count).ToList();
            var recipeList = new List<RecipePreviewRecipeModel>();

            //var result = foundRecipes.GroupBy(x => x.Blog);
            foreach (var recipe in foundRecipes)
            {
                var recipeModel = new RecipePreviewRecipeModel
                {
                    Id = recipe.RecipeId.ToString(),
                    Blog = recipe.Blog,
                    Image_Url = recipe.RecipeImage,
                    Title = recipe.RecipeName,
                    Url = recipe.RecipeUrl
                };

                recipeList.Add(recipeModel);
            }

            return new RecipePreviewModel
            {
                Count = foundRecipes.Count,
                Recipes = recipeList
            };
        }

        public RecipeModel SelectRecipeModelByRecipeId(int recipeId)
        {
            var recipe = _recipeDao.SelectRecipeByRecipeId(recipeId);
            IParser blog = null;

            switch (recipe.Blog)
            {
                case "Fantazje kulinarne Magdy K.":
                     blog = new FantazjeMagdyKParser(recipe.RecipeUrl);
                     break;
                case "Kwestia Smaku":
                    blog = new KwestiaSmakuParser(recipe.RecipeUrl);
                    break;
                case "Moje Dietetyczne Fanaberie":
                    blog = new MojeDietetyczneFanaberieParser(recipe.RecipeUrl);
                    break;
            }

            var recipeModel = new RecipeModel()
            {
                Blog = recipe.Blog,
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