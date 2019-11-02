using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RecipeSearch.Models.SearchRecipe
{
    public class SearchRecipeModel
    {
        public string Search { get; set; }
        public int Count { get; set; }
        public int[] DishIds { get; set; }
        public int[] dishSubCategoryIds { get; set; }
        public int[] dishMainCategoryIds { get; set; }
        public int[] ingredientIds { get; set; }
        public int[] ingredientCategoryIds { get; set; }
        public int[] ingredientCategoryXrefIds { get; set; }
    }
}