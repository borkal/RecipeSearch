using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int IngredientParentId { get; set; }
        public int IngredientQuantity { get; set; } //default??
        public bool IngredientCitrus { get; set; }
        public bool IngredientNut { get; set; }
        public bool IngredientSugar { get; set; }
        public int IngredientMushroom { get; set; }
        public bool IngredientGluten { get; set; }
        public bool IngredientCowMilk { get; set; }
        public bool IngredientWheat { get; set; }
        public bool IngredientVegetarian { get; set; }
        public IngredientCategory IngredientCategory { get; set; }
        public string IngredientAlternativeName { get; set; }
    }
}
