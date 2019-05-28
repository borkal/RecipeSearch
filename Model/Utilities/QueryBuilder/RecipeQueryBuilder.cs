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

        public string SelectRecipesByBlogName(string blogName)
        {
            //int test = 2130; //testowo - normalnie trzeba obsluzyc nieistniejace przepisy!!!
            //int test2 = 2212;
            //int test3 = 1375;
            //int test4 = 1418;
            //int test5 = 1414;
            //int test6 = 1428;
            var query = "SELECT " +
                        "R.id," +
                        "R.comments," +
                        "R.createdate," +
                        "R.image," +
                        "R.name," +
                        "R.url," +
                        "R.status " +
                        "FROM recipe R " +
                        "LEFT JOIN recipesource RS " +
                        "ON R.source_id = RS.id " +
                        $"WHERE rs.name LIKE '{blogName}'";
                        //$"AND R.id != '{test}'" +
                        //$"AND R.id != '{test2}'" +
                        //$"AND R.id != '{test3}'" +
                        //$"AND R.id != '{test4}'" +
                        //$"AND R.id != '{test5}'" +
                        //$"AND R.id != '{test6}'";
            return query;
        }

        public string SelecAllRecipeNamesAndIds() //testowa metoda
        {
            var query = "SELECT " +
                        "R.id," +
                        "R.name " +
                        "FROM recipe R";

            return query;
        }
    }
}
