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
                        "Replace(Replace((CAST(array_agg(distinct IC.categories_id) AS VARCHAR)),'}',''), '{', '') as ingredientCategoryIds," +
                        "I.name," +
                        "I.citrus," +
                        "I.nut," +
                        "I.sugar," +
                        "I.mushroom," +
                        "I.gluten," +
                        "I.cowmilk," +
                        "I.wheat," +
                        "I.egg," +
                        "I.vegetarian " +
                        "FROM ingredient I " +
                        "LEFT JOIN ingredient_ingredientcategory IC on IC.ingredient_id = I.id " +
                        "GROUP BY I.id";

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
