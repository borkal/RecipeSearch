using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.QueryBuilder
{
    public class IngredientQueryBuilder
    {
        public string SelectIngredients()
        {
            var query = "SELECT " +
                        "I.id," +
                        "I.name," +
                        "I.defaultquantity," +
                        "I.citrus," +
                        "I.nut," +
                        "I.sugar," +
                        "I.mushroom," +
                        "I.gluten,I.cowmilk," +
                        "I.wheat," +
                        "I.egg," +
                        "I.vegetarian " +
                        "FROM ingredient I " +
                        "LEFT JOIN recipeelement RE ON RE.ingredient_id = I.id " +
                        "LEFT JOIN recipe R ON R.id = RE.recipe_id ";

            return query;
        }

        public string SelectIngriedentCategoires()
        {
            return "SELECT " +
                   "IC.id, " +
                   "IC.name " +
                   "FROM ingredientcategory IC";
        }
        public string SelectIngriedentCategoiresXref()
        {
            return "SELECT " +
                   "IC.ingredient_id, " +
                   "IC.categories_id " +
                   "FROM ingredient_ingredientcategory IC";
        }
    }
}
