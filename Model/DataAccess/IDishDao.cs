using Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public interface IDishDao
    {
        List<Dish> SelectDishes();
        List<DishSubCategory> SelectDishSubCategories();
        List<DishMainCategory> SelectDishMainCategories();
    }
}
