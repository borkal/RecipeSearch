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
        public int DishId { get; set; }
        public int DishSubCategoryId { get; set; }
        public int DishMainCategoryId { get; set; }
        public List<int> IngredientIds { get; set; }
        public List<int> IngredientCategoryIds { get; set; }
        public List<int> FeatureIds { get; set; }
        public List<int> FeatureCategoryIds { get; set; }
        
    }
}