using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeSearch.Models.Ingredient
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> CategoryIds { get; set; }
        public bool Citrus { get; set; }
        public bool Nut { get; set; }
        public bool Sugar { get; set; }
        public bool Mushroom { get; set; }
        public bool Gluten { get; set; }
        public bool CowMilk { get; set; }
        public bool Wheat { get; set; }
        public bool Egg { get; set; }
        public bool Vegetarian { get; set; }
    }
}