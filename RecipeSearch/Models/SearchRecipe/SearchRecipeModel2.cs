using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RecipeSearch.Models.SearchRecipe
{
    public class SearchRecipeModel2
    {
        public string Search { get; set; }

        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;
        //public int PageSize = 5;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        //public int Count { get; set; }
        public int[] DishIds { get; set; }
        public int[] DishSubCategoryIds { get; set; }
        public int[] DishMainCategoryIds { get; set; }
        public int[] IngredientIds { get; set; }
        public int[] IngredientCategoryIds { get; set; }
        public int[] FeatureIds { get; set; }
        public int[] FeatureCategoryIds { get; set; }
        public bool? Citrus { get; set; }
        public bool? Nut { get; set; }
        public bool? Sugar { get; set; }
        public bool? Mushroom { get; set; }
        public bool? Gluten { get; set; }
        public bool? CowMilk { get; set; }
        public bool? Wheat { get; set; }
        public bool? Egg { get; set; }
        public bool? Vegetarian { get; set; }

    }
}