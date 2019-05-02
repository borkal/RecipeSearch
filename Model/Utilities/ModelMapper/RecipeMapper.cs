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
        public List<int> SearchRecipeIdsBasedOnSearchTextMapper(OdbcDataReader dataReader)
        {
            var recipeIds = new List<int>();
            while (dataReader.Read())
            {
                recipeIds.Add(dataReader.GetInt32(0));
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

            }

            return recipe;
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
    }
}
