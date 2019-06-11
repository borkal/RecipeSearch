using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;

namespace Model.Utilities.ModelMapper
{
    public class RecipeMapper
    {
        public Dictionary<int, string> SearchRecipeIdsBasedOnSearchTextMapper(OdbcDataReader dataReader)
        {
            var recipeIds = new Dictionary<int, string>();
            while (dataReader.Read())
            {
                recipeIds.Add(dataReader.GetInt32(0), dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1));
            }
            return recipeIds;
        }

        public Recipe SelectRecipeByRecipeIdMapper(OdbcDataReader dataReader)
        {
            var recipe = new Recipe();
            while (dataReader.Read()) 
            {

                recipe.RecipeId = dataReader.GetInt32(0);
                recipe.RecipeComments = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                recipe.RecipeCreateDate = dataReader[2] == DBNull.Value ? DateTime.MinValue : dataReader.GetDateTime(2);
                recipe.RecipeImage = dataReader[3] == DBNull.Value ? "" : dataReader.GetString(3);
                recipe.RecipeName = dataReader[4] == DBNull.Value ? "" : dataReader.GetString(4);
                recipe.RecipeUrl = dataReader[5] == DBNull.Value ? "" : dataReader.GetString(5);
                recipe.RecipeStatus = dataReader[6] == DBNull.Value ? 0 : dataReader.GetInt32(6);
                recipe.Blog_Url = dataReader[7] == DBNull.Value ? "" : dataReader.GetString(7);
                recipe.Blog = dataReader[8] == DBNull.Value ? "" : dataReader.GetString(8);

            }

            return recipe;
        }

        public List<Recipe> SelectRecipesByBlogNameMapper(OdbcDataReader dataReader)
        {
            var recipeList = new List<Recipe>();

            while (dataReader.Read())
            {
                Recipe recipeToList = new Recipe();
                recipeToList.RecipeId = dataReader.GetInt32(0);
                recipeToList.RecipeComments = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                recipeToList.RecipeCreateDate = dataReader[2] == DBNull.Value ? DateTime.MinValue : dataReader.GetDateTime(2);
                recipeToList.RecipeImage = dataReader[3] == DBNull.Value ? "" : dataReader.GetString(3);
                recipeToList.RecipeName = dataReader[4] == DBNull.Value ? "" : dataReader.GetString(4);
                recipeToList.RecipeUrl = dataReader[5] == DBNull.Value ? "" : dataReader.GetString(5);
                recipeToList.RecipeStatus = dataReader[6] == DBNull.Value ? 0 : dataReader.GetInt32(6);

                recipeList.Add(recipeToList);

            }

            return recipeList;
        }

        public Dictionary<int, string> SelecAllRecipeNamesAndIds(OdbcDataReader dataReader)
        {
            var recipeIdNameList = new Dictionary<int, string>();
            while (dataReader.Read())
            {
                recipeIdNameList.Add(
                    dataReader.GetInt32(0),
                    dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1));
            }

            return recipeIdNameList;
        }

        public string SelectBlogNameByRecipeId(OdbcDataReader dataReader)
        {
            var blogName = "";
            while (dataReader.Read())
            {
                blogName = dataReader[0] == DBNull.Value ? "" : dataReader.GetString(0);
            }

            return blogName;
        }

        public List<Recipe> SelectAlLRecipesBySearchText(OdbcDataReader dataReader)
        {
            var recipeList = new List<Recipe>();
            while (dataReader.Read())
            {
                var recipe = new Recipe();
                recipe.RecipeId = dataReader.GetInt32(0);
                recipe.RecipeComments = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                recipe.RecipeCreateDate = dataReader[2] == DBNull.Value ? DateTime.MinValue : dataReader.GetDateTime(2);
                recipe.RecipeImage = dataReader[3] == DBNull.Value ? "" : dataReader.GetString(3);
                recipe.RecipeName = dataReader[4] == DBNull.Value ? "" : dataReader.GetString(4);
                recipe.RecipeUrl = dataReader[5] == DBNull.Value ? "" : dataReader.GetString(5);
                recipe.RecipeStatus = dataReader[6] == DBNull.Value ? 0 : dataReader.GetInt32(6);
                recipe.Blog_Url = dataReader[7] == DBNull.Value ? "" : dataReader.GetString(7);
                recipe.Blog = dataReader[8] == DBNull.Value ? "" : dataReader.GetString(8);

                recipeList.Add(recipe);

            }

            return recipeList;
        }
    }
}
