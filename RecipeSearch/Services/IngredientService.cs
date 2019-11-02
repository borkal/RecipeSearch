using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Model.DataAccess;
using Model.Domain;

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
        public async Task<List<Ingredient>> SelectIngredients()
        {
            return _ingredientDao.SelectIngredients();
        }

        public async Task<List<IngredientCategory>> SelectIngredientCategories()
        {
            return _ingredientDao.SelectIngredientCategories();
        }

        public async Task<List<IngredientCategoryXref>> SelectIngredientCategoryXref()
        {
            return _ingredientDao.SelectIngriedentCategoiresXref();
        }
    }
}