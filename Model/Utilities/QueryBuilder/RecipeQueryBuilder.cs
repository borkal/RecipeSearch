using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.QueryBuilder
{
    public class RecipeQueryBuilder
    {
        public string SearchRecipeIdsBasedOnSearchText(string searchString)
        {
            var query = "SELECT " +
                        "R.id " +
                        "FROM recipe R " +
                        "LEFT JOIN recipeelement RE ON R.Id = RE.recipe_id " +
                        "LEFT JOIN ingredient I on RE.ingredient_id = I.id " +
                        $"WHERE R.name like '%{searchString}%' OR I.name like '%{searchString}%'";

            return query;
        }

        public string SelectRecipeByRecipeId(int recipeId)
        {
            var query = "SELECT " +
                        "R.id," +
                        "R.comments," +
                        "R.createdate," +
                        "R.image," +
                        "R.name," +
                        "R.url," +
                        "R.status " +
                        "FROM recipe R " +
                        $"WHERE R.id = {recipeId}";
            return query;
        }

        public string SelecAllRecipeNamesAndIds()
        {
            var query = "SELECT " +
                        "R.id," +
                        "R.name " +
                        "FROM recipe R";

            return query;
        }
    }
}
