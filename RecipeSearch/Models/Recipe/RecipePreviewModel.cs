using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeSearch.Models
{
    public class RecipePreviewModel
    {
        public string Id { get; set; }
        public string Blog { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Image_Url { get; set; }
        public List<int> DishIds { get; set; }
        public List<int> DishSubCategoryIds { get; set; }
        public List<int> DishMainCategoryIds { get; set; }

        public List<int> IngredientIds { get; set; }
        public List<int> IngredientCategoryIds { get; set; }
       

        //public List<Dish> Dishes2 { get; set; }
        //public List<DishSubCategory> DishSubCategories2 { get; set; }
        //public List<DishMainCategory> DishMainCategories2 { get; set; }
    }
}