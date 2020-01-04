using Model.Domain.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain.Recipe
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeComments { get; set; }
        public DateTime RecipeCreateDate { get; set; }
        public string RecipeImage { get; set; }
        public string RecipeName { get; set; }
        public string RecipeUrl { get; set; }
        public Dish RecipeDish { get; set; }
        public int RecipeAuthorId { get; set; }
        public RecipeSource RecipeSourceId { get; set; }
        public int RecipeStatus { get; set; }
        public string Blog_Url { get; set; }
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public int DishId { get; set; }
        public int DishSubCategoryId { get; set; }
        public int DishMainCategoryId { get; set; }
        public List<int> IngredientIds { get; set; }
        public List<int> IngredientCategoryIds { get; set; }
        public List<int> FeatureIds { get; set; }
        public List<int> FeatureCategoryIds { get; set; }
        public List<string> Rates { get; set; }
        public TotalRate TotalRecipeRate { get; set; }
        public void CalculateTotalRecipeRate()
        {
            if (Rates.Count != 0)
            {
                TotalRecipeRate = new TotalRate
                {
                    RateAverage = Convert.ToInt32(Rates.Select(x => Convert.ToInt32(x)).Average()),
                    RateAmounts = Rates.Count
                };

            }
        }
    }
}
