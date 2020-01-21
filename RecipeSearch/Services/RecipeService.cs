using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.WebPages.Scope;
using Model.DataAccess;
using Model.Domain;
using Model.Domain.Recipe;
using Model.Enums;
using Model.Utilities.Parsers;
using RecipeSearch.Models;
using RecipeSearch.Models.Recipe;
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
            var foundRecipes = _recipeDao.SelectAlLRecipesBySearchText(new SearchRecipe
            {
                Search = searchRecipeModel.Search,
                Count = searchRecipeModel.Count,
                DishIds = searchRecipeModel.DishIds,
                DishSubCategoryIds = searchRecipeModel.DishSubCategoryIds,
                DishMainCategoryIds = searchRecipeModel.DishMainCategoryIds,
                IngredientIds = searchRecipeModel.IngredientIds,
                IngredientCategoryIds = searchRecipeModel.IngredientCategoryIds,
                FeatureIds = searchRecipeModel.FeatureIds,
                FeatureCategoryIds = searchRecipeModel.FeatureCategoryIds,
                Citrus = searchRecipeModel.Citrus,
                Nut = searchRecipeModel.Nut,
                Sugar = searchRecipeModel.Sugar,
                Mushroom = searchRecipeModel.Mushroom,
                Gluten = searchRecipeModel.Gluten,
                CowMilk = searchRecipeModel.CowMilk,
                Wheat = searchRecipeModel.Wheat,
                Egg = searchRecipeModel.Egg,
                Vegetarian = searchRecipeModel.Vegetarian


            }).ToList();

            foundRecipes = EliminateAndOrCategoriesProblem(foundRecipes, searchRecipeModel);
            PopulateRecipeRates(foundRecipes);

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

                recipeModel.Rate = recipe.TotalRecipeRate != null
                    ? new RecipeTotalRate
                    {
                        Amount = recipe.TotalRecipeRate.RateAmounts,
                        Average = recipe.TotalRecipeRate.RateAverage
                    }
                    : new RecipeTotalRate { Average = 0, Amount = 0 };

                recipeList.Add(recipeModel);
            }

            return recipeList;
        }
        internal async Task<List<RecipePreviewModel>> SelectRecipePreviewModelBySearchTextPaged(SearchRecipeModel2 searchRecipeModel)
        {
            var foundRecipes = _recipeDao.SelectAlLRecipesBySearchTextPaged(new SearchRecipe2
            {
                Search = searchRecipeModel.Search,
                PageNumber = searchRecipeModel.PageNumber,
                PageSize = searchRecipeModel.PageSize,
                DishIds = searchRecipeModel.DishIds,
                DishSubCategoryIds = searchRecipeModel.DishSubCategoryIds,
                DishMainCategoryIds = searchRecipeModel.DishMainCategoryIds,
                IngredientIds = searchRecipeModel.IngredientIds,
                IngredientCategoryIds = searchRecipeModel.IngredientCategoryIds,
                FeatureIds = searchRecipeModel.FeatureIds,
                FeatureCategoryIds = searchRecipeModel.FeatureCategoryIds,
                Citrus = searchRecipeModel.Citrus,
                Nut = searchRecipeModel.Nut,
                Sugar = searchRecipeModel.Sugar,
                Mushroom = searchRecipeModel.Mushroom,
                Gluten = searchRecipeModel.Gluten,
                CowMilk = searchRecipeModel.CowMilk,
                Wheat = searchRecipeModel.Wheat,
                Egg = searchRecipeModel.Egg,
                Vegetarian = searchRecipeModel.Vegetarian
            }).ToList();

            foundRecipes = EliminateAndOrCategoriesProblem(foundRecipes, searchRecipeModel);
            PopulateRecipeRates(foundRecipes);

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
                    FeatureCategoryIds = recipe.FeatureCategoryIds,
                };

                recipeModel.Rate = recipe.TotalRecipeRate != null
                    ? new RecipeTotalRate
                    {
                        Amount = recipe.TotalRecipeRate.RateAmounts,
                        Average = recipe.TotalRecipeRate.RateAverage
                    }
                    : new RecipeTotalRate { Average = 0, Amount = 0 };

                recipeList.Add(recipeModel);
            }

            return recipeList;
        }

        internal async Task<RecipeModel> SelectRecipeModelByRecipeId(int recipeId)
        {
            var recipe = _recipeDao.SelectRecipeByRecipeId(recipeId);
            List<string> recipeIngredients = _recipeDao.SelectRecipeIngredientsFromDatabase(recipeId);
            List<string> recipeDescription = _recipeDao.SelectRecipeDescriptionFromDatabase(recipeId);

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

            PopulateRecipeRates(new List<Recipe>() { recipe });

            var recipeModel = new RecipeModel
            {
                Blog = recipe.BlogName,
                Blog_Url = recipe.Blog_Url,
                Description = recipeDescription.Any() ? recipeDescription : blog != null ? blog.GetDescription() : new List<string>(),
                Image_Url = recipe.RecipeImage,
                Ingredients = recipeIngredients.Any() ? recipeIngredients : blog != null ? blog.GetIngredients() : new List<string>(),
                Id = recipeId.ToString(),
                Source_Url = recipe.RecipeUrl,
                Title = recipe.RecipeName,
                DishId = recipe.DishId,
                DishSubCategoryId = recipe.DishSubCategoryId,
                DishMainCategoryId = recipe.DishMainCategoryId,
                IngredientIds = recipe.IngredientIds,
                IngredientCategoryIds = recipe.IngredientCategoryIds,
                FeatureIds = recipe.FeatureIds,
                FeatureCategoryIds = recipe.FeatureCategoryIds,
                Rate = new RecipeTotalRate
                {
                    Amount = recipe.TotalRecipeRate.RateAmounts,
                    Average = recipe.TotalRecipeRate.RateAverage
                }
            };

            return recipeModel;
        }

        internal async Task InsertRecipeRateIntoDatabase(int recipeId, int rate, string username)
        {
            _recipeDao.InsertRecipeRateIntoDatabase(recipeId, rate, username);
        }

        internal async Task<RecipeRates> SelectUserRateDataFromRateTable(int id, string username)
        {
            var recipeUserRate = _recipeDao.SelectUserRateDataFromRateTable(id, username);

            var recipeModel = new RecipeRates
            {
                recipeId = recipeUserRate.recipeId,
                recipeRate = recipeUserRate.recipeRate,
                userName = recipeUserRate.userName
            };

            return recipeModel;
        }
        private void PopulateRecipeRates(List<Recipe> recipes)
        {
            if (recipes.Count > 0)
            {
                var recipeIds = recipes.Select(x => x.RecipeId).ToList();
                var recipesRates = _recipeDao.SelectRecipeRates(recipeIds);

                recipes.ForEach(x =>
                {
                    x.Rates = recipesRates.Where(z => z.recipeId == x.RecipeId).Select(y => y.recipeRate.ToString()).ToList();
                    x.CalculateTotalRecipeRate();
                });
            }
        }

        private List<Recipe> EliminateAndOrCategoriesProblem(List<Recipe> foundRecipes, SearchRecipeModel searchRecipeModel)
        {
            var recipes = new List<Recipe>();

            foundRecipes = foundRecipes.Where(x => searchRecipeModel.FeatureIds.All(y => x.FeatureIds.Contains(y))).ToList();
            foundRecipes = foundRecipes.Where(x => searchRecipeModel.FeatureCategoryIds.All(y => x.FeatureCategoryIds.Contains(y))).ToList();

            foundRecipes = foundRecipes.Where(x => searchRecipeModel.IngredientCategoryIds.All(y => x.IngredientCategoryIds.Contains(y))).ToList();
            foundRecipes = foundRecipes.Where(x => searchRecipeModel.IngredientIds.All(y => x.IngredientIds.Contains(y))).ToList();

            recipes.AddRange(foundRecipes);
            return recipes;
        }

        private List<Recipe> EliminateAndOrCategoriesProblem(List<Recipe> foundRecipes, SearchRecipeModel2 searchRecipeModel)
        {
            var recipes = new List<Recipe>();

            foundRecipes = foundRecipes.Where(x => searchRecipeModel.FeatureIds.All(y => x.FeatureIds.Contains(y))).ToList();
            foundRecipes = foundRecipes.Where(x => searchRecipeModel.FeatureCategoryIds.All(y => x.FeatureCategoryIds.Contains(y))).ToList();

            foundRecipes = foundRecipes.Where(x => searchRecipeModel.IngredientCategoryIds.All(y => x.IngredientCategoryIds.Contains(y))).ToList();
            foundRecipes = foundRecipes.Where(x => searchRecipeModel.IngredientIds.All(y => x.IngredientIds.Contains(y))).ToList();

            recipes.AddRange(foundRecipes);
            return recipes;
        }
    }



}