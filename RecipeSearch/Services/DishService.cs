using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Model.DataAccess;
using Model.Domain;
using RecipeSearch.Models.Dish;

namespace RecipeSearch.Services
{
    public class DishService
    {
        private readonly IDishDao _dishDao;

        public DishService(): this(new DishDao())
        {

        }

        public DishService(DishDao dishDao)
        {
            _dishDao = dishDao;
        }

        public async Task<List<DishModel>> SelectDishes()
        {
            return _dishDao.SelectDishes().Select(x => new DishModel()
            {
                Id = x.DishId,
                Name = x.DishName,
                SubCategoryId = x.DishSubCategoryId
           
            }).OrderBy(y => y.Id).ToList();
        }

        public async Task<List<DishSubCategoryModel>> SelectDishSubCategories()
        {
            return _dishDao.SelectDishSubCategories().Select(x => new DishSubCategoryModel()
            {
                Id =  x.DishSubCategoryId,
                Name = x.DishSubCategoryName,
                MainCategoryId = x.DishMainCategoryId

            }).OrderBy(y => y.Id).ToList();
        }

        public async Task<List<DishMainCategoryModel>> SelectDishMainCategories()
        {
            return _dishDao.SelectDishMainCategories().Select(x => new DishMainCategoryModel()
            {
                Id = x.DishMainCategoryId,
                Name = x.DishMainCategoryName

            }).OrderBy(y => y.Id).ToList();
        }
    }
}