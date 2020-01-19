using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Domain;
using Model.Domain.Recipe;


namespace RecipeSearch.Models.Recipe
{
    public class RecipePreviewModel2
    {
        public string Id { get; set; }
        public string Blog { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Image_Url { get; set; }
        public int DishId { get; set; }
        public int DishSubCategoryId { get; set; }
        public int DishMainCategoryId { get; set; }
        public List<int> IngredientIds { get; set; }
        public List<int> IngredientCategoryIds { get; set; }
        public List<int> FeatureIds { get; set; }
        public List<int> FeatureCategoryIds { get; set; }
        public RecipeTotalRate Rate { get; set; }

        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize;
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

    }
}