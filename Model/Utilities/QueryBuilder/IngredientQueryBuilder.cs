using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.QueryBuilder
{
    public class IngredientQueryBuilder
    {
        public string SelectAllIngredientsByRecipeId(int recipeId)
        {
            //var query = "SELECT " +
            //            "I.id," +
            //            "I.name," +
            //            "I.defaultquantity," +
            //            "I.citrus," +
            //            "I.nut," +
            //            "I.sugar," +
            //            "I.mushroom," +
            //            "I.gluten,I.cowmilk," +
            //            "I.wheat," +
            //            "I.egg," +
            //            "I.vegetarian " +
            //            "from recipe " +
            //            "LEFT JOIN recipeelement RE on R.id = RE.recipe_id " +
            //            "LEFT JOIN ingredient I on RE.ingredient_id = I.id " +
            //            $"WHERE R.Id = {recipeId} ";

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
                        "LEFT JOIN recipe R ON R.id = RE.recipe_id " +
                        $"WHERE R.Id = {recipeId} ";


            return query;
        }
    }
}
