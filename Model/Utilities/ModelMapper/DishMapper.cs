using Model.Domain;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.ModelMapper
{
    public class DishMapper
    {
        public List<Dish> SelectDishes(OdbcDataReader dataReader)
        {
            var dishList = new List<Dish>();
            while (dataReader.Read())
            {
                var dish = new Dish();
                dish.DishId = dataReader.GetInt32(0);
                dish.DishName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                dish.DishSubCategoryId = dataReader[2] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(2));

                dishList.Add(dish);
            }
            dataReader.Close();
            return dishList;
        }
        public List<DishSubCategory> SelectDishSubCategories(OdbcDataReader dataReader)
        {
            var dishList = new List<DishSubCategory>();
            while (dataReader.Read())
            {
                var dish = new DishSubCategory();
                dish.DishSubCategoryId = dataReader.GetInt32(0);
                dish.DishSubCategoryName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                dish.DishMainCategoryId = dataReader[2] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(2));

                dishList.Add(dish);
            }
            dataReader.Close();
            return dishList;
        }

        public List<DishMainCategory> SelectDishMainCategories(OdbcDataReader dataReader)
        {
            var dishList = new List<DishMainCategory>();
            while (dataReader.Read())
            {
                var dish = new DishMainCategory();
                dish.DishMainCategoryId = dataReader.GetInt32(0);
                dish.DishMainCategoryName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                dishList.Add(dish);
            }
            dataReader.Close();
            return dishList;
        }



    }
}
