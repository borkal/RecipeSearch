using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;

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
                "WHERE R" + "S.id in (1, 3, 4, 5, 7, 11) " +
               $"AND lower(I.name) like lower('%{searchString}%') " +
               $"OR lower(R.name) like lower('%{searchString}%') " +
                "GROUP BY R.id, RS.id";

            return query;
        }

        public string SelectRecipeByRecipeId(int recipeId)
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
                        "RS.id, RS.name, " +
                        "Replace(Replace((CAST(array_agg(RR.rate) AS VARCHAR)), '}', ''), '{', '') as reciperates, " +
                        "D.id as dishId, " +
                        "DS.id as dishSubCategoryId, " +
                        "DM.id as dishMainCategoryId, " +
                        "Replace(Replace((CAST(array_agg(distinct I2.id) AS VARCHAR)), '}', ''), '{', '') as ingredientIds, " +
                        "Replace(Replace((CAST(array_agg(distinct IC.id) AS VARCHAR)), '}', ''), '{', '') as ingridientCategoryIds, " +
                        "Replace(Replace((CAST(array_agg(distinct F2.id) AS VARCHAR)), '}', ''), '{', '') as featureIds, " +
                        "Replace(Replace((CAST(array_agg(distinct FC.id) AS VARCHAR)), '}', ''), '{', '') as featureCategoryIds " +
                        "FROM recipe R " +
                        "LEFT JOIN recipesource RS on R.source_id = RS.id " +
                        "LEFT JOIN recipeelement RE on R.id = RE.recipe_id " +
                        "LEFT JOIN reciperate RR on RR.recipeid = R.id " +
                        "LEFT JOIN dish D on R.dish_id = D.id " +
                        "LEFT JOIN dishCategory DS on D.category_id = DS.id " +
                        "LEFT JOIN dishCategory DM on DS.parent_id = DM.id " +
                        "LEFT JOIN ingredient I on RE.ingredient_id = I.id " +
                        "JOIN(SELECT RE2.recipe_id, I2.id, I2.name FROM recipeelement RE2 " +
                        "LEFT JOIN ingredient I2 on RE2.ingredient_id = I2.id) I2 on I2.recipe_id = RE.recipe_id " +
                        "LEFT JOIN ingredient_alternativenames IAN on I.id = IAN.ingredient_id " +
                        "LEFT JOIN ingredient_ingredientcategory Ixref on I2.id = Ixref.ingredient_id " +
                        "LEFT JOIN ingredientcategory IC on Ixref.categories_id = IC.id " +
                        "LEFT JOIN recipe_feature RF on RF.recipe_id = R.id " +
                        "LEFT JOIN feature F on RF.feature_id = F.id " +
                        "LEFT JOIN(SELECT RF2.recipe_id, F2.id, F2.category_id " +
                        "FROM recipe_feature RF2 " +
                        "LEFT JOIN feature F2 on RF2.feature_id = F2.id) F2 on F2.recipe_id = R.id " +
                        "LEFT JOIN featurecategory FC on FC.id = F2.category_id " +
                        $"WHERE R.id = {recipeId} " +
                        "GROUP BY R.id, RS.id, D.id, DS.id, DM.id";
                        
            return query;
        }

        public string SelectRecipeOfTheDayRowFromDatabase()
        {
            var check = "SELECT " +
                       "DR.date," +
                       "DR.recipe_id " +
                       "FROM dayrecipe DR " +
                       "ORDER BY DR.date DESC " +
                       "LIMIT 1";

            return check;
        }

        public string InsertRecipeOfTheDayRowToDatabase(string date, int recipeId)
        {
            return "INSERT INTO dayrecipe " +
            $"values('{date}', {recipeId})";
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

        public string InsertRecipeRateIntoDatabase(int recipeId, int rate, string username)
        {
            return "INSERT INTO reciperate " +
                   $"values({recipeId}, {rate}, '{username}')";
        }

        public string SelectUserRateDataFromRateTable(int id, string username)
        {
            var query = "SELECT " +
                        "RR.recipeid, "+
                        "RR.rate, "+
                        "RR.username " +
                        "FROM reciperate RR " +
                        $"WHERE RR.recipeid = {id} AND RR.username = '{username}'";
            return query;
        }

        public string SelectRecipesRates(List<int> recipeIds)
        {
            return "SELECT " +
                   "RR.recipeId," +
                   "RR.rate," +
                   "RR.username " +
                   "FROM reciperate RR " +
                   $"WHERE RR.recipeid in ({string.Join(",",recipeIds)})";
        }

        public string SelectRecipeIngredientsFromDatabase(int recipeId)
        {
            var query = "SELECT " +
                        "RI.ingredient_text " +
                        "FROM recipe_ingredient_display RI " +
                        $"WHERE RI.recipe_id = {recipeId}";

            return query;
        }

        public string SelectRecipeDescriptionFromDatabase(int recipeId)
        {
            var query = "SELECT " +
                        "RD.description_text " +
                        "FROM recipe_description_display RD " +
                        $"WHERE RD.recipe_id = {recipeId}" +
                        "ORDER BY RD.order_display ASC";

            return query;
        }

        public string SelectAlLRecipesBySearchText(SearchRecipe search)
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
            "Replace(Replace((CAST(array_agg(distinct I2.id) AS VARCHAR)),'}',''), '{', '') as ingredientIds, " +
            "Replace(Replace((CAST(array_agg(distinct IC.id) AS VARCHAR)),'}',''), '{', '') as ingridientCategoryIds, " +
            "Replace(Replace((CAST(array_agg(distinct F2.id) AS VARCHAR)),'}',''), '{', '') as featureIds, " +
            "Replace(Replace((CAST(array_agg(distinct FC.id) AS VARCHAR)),'}',''), '{', '') as featureCategoryIds, " +
            "Replace(Replace((CAST(array_agg(RR.rate) AS VARCHAR)),'}',''), '{', '') as reciperates " +
            "FROM recipe R " +
            "LEFT JOIN recipesource RS on R.source_id = RS.id " +
            "LEFT JOIN recipeelement RE on R.id = RE.recipe_id " +
            "LEFT JOIN (select RR.recipeid, RR.rate from reciperate RR) RR on RR.recipeid = RE.id  " +
            "LEFT JOIN dish D on R.dish_id = D.id " +
            "LEFT JOIN dishCategory DS on D.category_id = DS.id " +
            "LEFT JOIN dishCategory DM on DS.parent_id = DM.id " +
            "LEFT JOIN ingredient I on RE.ingredient_id = I.id " +
            "JOIN " +
                    "(SELECT RE2.recipe_id, I2.id, I2.name " +
                    "FROM recipeelement RE2 " +
                    "LEFT JOIN ingredient I2 on RE2.ingredient_id = I2.id) I2 on I2.recipe_id = RE.recipe_id " +
            "LEFT JOIN ingredient_alternativenames IAN on I.id = IAN.ingredient_id " +
            "LEFT JOIN ingredient_ingredientcategory Ixref on I2.id = Ixref.ingredient_id " +
            "LEFT JOIN ingredientcategory IC on Ixref.categories_id = IC.id " +
            "LEFT JOIN recipe_feature RF on RF.recipe_id = R.id " +
            "LEFT JOIN feature F on RF.feature_id = F.id " +
            "LEFT JOIN " +
                    "(SELECT RF2.recipe_id, F2.id, F2.category_id " +
                    "FROM recipe_feature RF2 " +
                    "LEFT JOIN feature F2 on RF2.feature_id = F2.id) F2 on F2.recipe_id = R.id " +
            "LEFT JOIN featurecategory FC on FC.id = F2.category_id " +
            "WHERE RS.id in (1, 3, 4, 5, 7, 11) " +
            (search.DishIds.Any() ? $"AND D.id in ({String.Join(",", search.DishIds.Select(x => x.ToString()))}) " : "") +
            (search.DishSubCategoryIds.Any() ? $"AND DS.id in ({String.Join(",", search.DishSubCategoryIds.Select(x => x.ToString()))}) " : "") +
            (search.DishMainCategoryIds.Any() ? $"AND DM.id in ({String.Join(",", search.DishMainCategoryIds.Select(x => x.ToString()))}) " : "")  +
            (search.IngredientIds.Any() ? $"AND I.id in ({String.Join(",", search.IngredientIds.Select(x => x.ToString()))}) " : "") +
            (search.IngredientCategoryIds.Any() ? $"AND IC.id in ({String.Join(",", search.IngredientCategoryIds.Select(x => x.ToString()))}) " : "") +
            (search.FeatureIds.Any() ? $"AND F.id in ({String.Join(",", search.FeatureIds.Select(x => x.ToString()))}) " : "") +
            (search.FeatureCategoryIds.Any() ? $"AND FC.id in ({String.Join(",", search.FeatureCategoryIds.Select(x => x.ToString()))}) " : "") +
            $"AND (" +
            $"LOWER(I.name) like LOWER('%{search.Search}%') " +
            $"OR LOWER(R.name) like LOWER('%{search.Search}%') " +
            $"OR LOWER(IAN.alternativenames) like LOWER('%{search.Search}%')" +
            ") " +
            "GROUP BY R.id, RS.id, D.id, DS.id, DM.id " +
            "HAVING COUNT(DISTINCT I2.id) = (SELECT COUNT(DISTINCT I3.id) FROM recipeelement RE2 LEFT JOIN ingredient I3 on RE2.ingredient_id = I3.id WHERE RE2.recipe_id = R.id " +
            (search.Citrus != null ? $"AND I3.citrus = {search.Citrus} " : "") +
            (search.Nut != null ? $"AND I3.nut = {search.Nut} " : "") +
            (search.Mushroom != null ? $"AND I3.mushroom = {search.Mushroom} " : "") +
            (search.Gluten != null ? $"AND I3.gluten = {search.Gluten} " : "") +
            (search.CowMilk != null ? $"AND I3.cowmilk = {search.CowMilk} " : "") +
            (search.Wheat != null ? $"AND I3.wheat = {search.Wheat} " : "") +
            (search.Egg != null ? $"AND I3.egg = {search.Egg} " : "") +
            (search.Vegetarian != null ? $"AND I3.vegetarian = {search.Vegetarian} " : "") +
            ") " +
            $"LIMIT {search.Count}";
                
            return query;
        }
    }
}