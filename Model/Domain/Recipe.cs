using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeComments { get; set; }
        public DateTime RecipeCreateDate { get; set; }
        public string RecipeImage { get; set; }
        public string RecipeName { get; set; }
        public string RecipeUrl { get; set; }
        public int DishId { get; set; }
        public Dish RecipeDish { get; set; }
        public int RecipeAuthorId { get; set; }
        public RecipeSource RecipeSourceId { get; set; }
        public int RecipeStatus { get; set; }
        public List<Ingredient> RecipeIngredientsList { get; set; }
        //parsery do sprawdzenia, jak pobierają przepisy?? skąd opis przepisów, kroki?
        public List<Feature> RecipeFeature { get; set; }
    }
}
