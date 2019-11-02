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
            var query =
                "SELECT R.id, " +
                "RS.name " +
                "FROM recipe R " +
                "LEFT JOIN recipesource RS on R.source_id = RS.id " +
                "LEFT JOIN recipeelement RE ON R.Id = RE.recipe_id " +
                "LEFT JOIN ingredient I on RE.ingredient_id = I.id " +
                "WHERE R" +"S.id in (1, 3, 5) " +
               $"AND lower(I.name) like lower('%{searchString}%') " +
               $"OR lower(R.name) like lower('%{searchString}%') " +
                "GROUP BY R.id, RS.id";

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
                        "R.status, " +
                        "RS.canonicalurl, " +
                        "RS.id, " +
                        "RS.name " +
                        "FROM recipe R  " +
                        "LEFT JOIN recipesource RS on R.source_id = RS.id " +
                        $"WHERE R.id = {recipeId}";
            return query;
        }

        public string SelectRecipesByBlogId(int blogId)
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
                        "LEFT JOIN recipesource RS " +
                        "ON R.source_id = RS.id " +
                        $"WHERE RS.id = {blogId} " +
                        $"AND NOT EXISTS (SELECT * FROM recipe_ingredient_display rid where rid.recipe_id = R.id) " +
                        $"";
                       
            return query;
        }

        public string InsertRecipeIngredient(int recipeId, string ingredientText)
        {
            return "INSERT INTO recipe_ingredient_display " +
                  $"values({recipeId},'{ingredientText}')";
        }

        public string InsertRecipeDescription(int recipeId, string descriptionText, int orderId)
        {
            return "INSERT INTO recipe_description_display " +
                   $"values({recipeId},'{descriptionText}',{orderId})";
        }

        public string SelecAllRecipeNamesAndIds() //testowa metoda
        {
            var query = "SELECT " +
                        "R.id," +
                        "R.name " +
                        "FROM recipe R";

            return query;
        }

        public string SelectBlogNameByRecipeId(int recipeId)
        {
            var query = "SELECT RS.name " +
                        "from recipe R " +
                        "left join recipesource RS on R.source_id = RS.id " +
                       $"where R.id = {recipeId}";
            return query;

        }

        public string SelectAlLRecipesBySearchText(string searchText, int[] dishIds, int[] dishSubCategoryIds, int[] dishMainCategoryIds, int[] ingredientIds, int[] ingredientCategoryIds)
        {
            var query = "SELECT " + 
            "R.id, " +
            "R.comments, " +
            "R.createdate, " +
            "R.image, " +
            "R.name, " +
            "R.url, " +
            "R.status, " +
            "RS.canonicalurl, " +
            "RS.name, " +
            "D.id as dishId, " +
            "DS.id as dishSubCategoryId, " +
            "DM.id as dishMainCategoryId, " +
            "Replace(Replace((CAST(array_agg(distinct I.id) AS VARCHAR)),'}',''), '{', '') as ingredientIds, " +
            "Replace(Replace((CAST(array_agg(distinct IC.id) AS VARCHAR)),'}',''), '{', '') as ingridientCategoryIds " +
            "FROM recipe R " +
            "LEFT JOIN recipesource RS on R.source_id = RS.id " +
            "LEFT JOIN recipeelement RE on R.id = RE.recipe_id " +
            "LEFT JOIN dish D on R.dish_id = D.id " +
            "LEFT JOIN dishCategory DS on D.category_id = DS.id " +
            "LEFT JOIN dishCategory DM on DS.parent_id = DM.id " +
            "LEFT JOIN ingredient I on RE.ingredient_id = I.id " +
            "LEFT JOIN ingredient_alternativenames IAN on I.id = IAN.ingredient_id " +
            "LEFT JOIN ingredient_ingredientcategory Ixref on I.id = Ixref.ingredient_id " +
            "LEFT JOIN ingredientcategory IC on Ixref.categories_id = IC.id " +
            "WHERE RS.id in (1, 3, 5) " +
            (ingredientIds.Any() ? $"AND I.id in ({String.Join(",", ingredientIds.Select(x => x.ToString()))}) " : "") +
            (ingredientCategoryIds.Any() ? $"AND IC.id in ({String.Join(",", ingredientCategoryIds.Select(x => x.ToString()))}) " : "") +
            (dishIds.Any() ? $"AND D.id in ({String.Join(",", dishIds.Select(x => x.ToString()))}) " : "") +
            (dishSubCategoryIds.Any() ? $"AND DS.id in ({String.Join(",", dishSubCategoryIds.Select(x => x.ToString()))}) " : "") +
            (dishMainCategoryIds.Any() ? $"AND DM.id in ({String.Join(",", dishMainCategoryIds.Select(x => x.ToString()))}) " : "")  +
            $"AND (" +
            $"LOWER(I.name) like LOWER('%{searchText}%') " +
            $"OR LOWER(R.name) like LOWER('%{searchText}%') " +
            $"OR LOWER(IAN.alternativenames) like LOWER('%{searchText}%')" +
            ") " +
            "GROUP BY R.id, RS.id, D.id, DS.id, DM.id";
                
            return query;
        }
    }
}