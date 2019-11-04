using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Model.DataAccess;
using Model.Domain;
using RecipeSearch.Models.Ingredient;

namespace RecipeSearch.Services
{
    public class IngredientService
    {
        private readonly IIngredientDao _ingredientDao;

        public IngredientService() : this(new IngredientDao())
        {

        }

        public IngredientService(IIngredientDao ingredientDao)
        {
            _ingredientDao = ingredientDao;
        }
        public async Task<List<IngredientModel>> SelectIngredients()
        {
            return _ingredientDao.SelectIngredients().Select(x => new IngredientModel()
            {
                Id = x.IngredientId,
                CategoryIds = x.IngredientCategoryIds,
                Name = x.IngredientName,
                Citrus = x.IngredientCitrus,
                CowMilk = x.IngredientCowMilk,
                Egg = x.IngredientEgg,
                Gluten = x.IngredientGluten,
                Mushroom = x.IngredientMushroom,
                Nut = x.IngredientNut,
                Sugar = x.IngredientSugar,
                Wheat = x.IngredientWheat,
                Vegetarian = x.IngredientVegetarian

            }).ToList();
        }

        public async Task<List<IngredientCategoryModel>> SelectIngredientCategories()
        {
            return _ingredientDao.SelectIngredientCategories().Select(x => new IngredientCategoryModel()
            {
                Id = x.IngredientCategoryId,
                Name = x.IngredientCategoryName
            }).ToList();
        }

        public async Task<List<IngredientCategoryXref>> SelectIngredientCategoryXref()
        {
            return _ingredientDao.SelectIngriedentCategoiresXref();
        }
    }
}