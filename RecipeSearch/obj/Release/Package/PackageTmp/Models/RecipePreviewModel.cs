using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeSearch.Models
{
    public class RecipePreviewModel
    {
        public int Count { get; set; }
        public List<RecipePreviewRecipeModel> Recipes { get; set; }
    }
}