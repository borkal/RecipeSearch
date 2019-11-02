using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeSearch.Models.Dish
{
    public class DishSubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainCategoryId { get; set; }
    }
}