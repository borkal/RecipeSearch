﻿using System;
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
        public List<string> SelectRecipeIngredientsFromDatabaseMapper(OdbcDataReader dataReader)
        {
            var IngredientsList = new List<string>();
            while (dataReader.Read())
            {
                IngredientsList.Add(dataReader.GetString(0));
            }

            dataReader.Close();
            return IngredientsList;
        }
        public List<string> SelectRecipeDescriptionFromDatabaseMapper(OdbcDataReader dataReader)
        {
            var DescriptionList = new List<string>();
            while (dataReader.Read())
            {
                DescriptionList.Add(dataReader.GetString(0));
            }
            dataReader.Close();
            return DescriptionList;
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
                recipe.BlogId = dataReader[8] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(8));
                recipe.BlogName = dataReader[9] == DBNull.Value ? "" : dataReader.GetString(9);

            }
            dataReader.Close();
            return recipe;
        }

        public Recipe SelectRandomRecipeMapper(OdbcDataReader dataReader)
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
                recipe.BlogId = dataReader[8] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(8));
                recipe.BlogName = dataReader[9] == DBNull.Value ? "" : dataReader.GetString(9);

            }
            dataReader.Close();
            return recipe;
        }

        public DayRecipe SelectRecipeOfTheDayRowFromDatabaseMapper(OdbcDataReader dataReader)
        {
            var dayRecipe = new DayRecipe();

            while (dataReader.Read())
            {
                dayRecipe.DayRecipeDate = dataReader.GetString(0);
                dayRecipe.DayRecipeRecipeId = dataReader.GetInt32(1);
            }

            return dayRecipe;
        }

        public Recipe SelectDayRecipeMapper(OdbcDataReader dataReader)
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
                recipe.BlogId = dataReader[8] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(8));
                recipe.BlogName = dataReader[9] == DBNull.Value ? "" : dataReader.GetString(9);

            }

            return recipe;
        }

        public List<Recipe> SelectRecipesByBlogIdMapper(OdbcDataReader dataReader)
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
            dataReader.Close();
            return recipeList;
        }

        public List<Recipe> SelectAlLRecipesBySearchText(OdbcDataReader dataReader)
        {
            var recipeList = new List<Recipe>();
            while (dataReader.Read())
            {
                var recipe = new Recipe();
                try
                {
                    recipe.RecipeId = dataReader.GetInt32(0);
                    recipe.RecipeComments = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                    recipe.RecipeCreateDate = dataReader[2] == DBNull.Value ? DateTime.MinValue : dataReader.GetDateTime(2);
                    recipe.RecipeImage = dataReader[3] == DBNull.Value ? "" : dataReader.GetString(3);
                    recipe.RecipeName = dataReader[4] == DBNull.Value ? "" : dataReader.GetString(4);
                    recipe.RecipeUrl = dataReader[5] == DBNull.Value ? "" : dataReader.GetString(5);
                    recipe.RecipeStatus = dataReader[6] == DBNull.Value ? 0 : dataReader.GetInt32(6);
                    recipe.Blog_Url = dataReader[7] == DBNull.Value ? "" : dataReader.GetString(7);
                    recipe.BlogName = dataReader[8] == DBNull.Value ? "" : dataReader.GetString(8);
                    recipe.DishId = dataReader[9] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(9));
                    recipe.DishSubCategoryId = dataReader[10] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(10));
                    recipe.DishMainCategoryId = dataReader[11] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(11));
                    recipe.IngredientIds = dataReader[12] == DBNull.Value ? new List<int>() : ConvertStringToIntList(dataReader.GetValue(12).ToString());
                    recipe.IngredientCategoryIds = dataReader[13] == DBNull.Value ? new List<int>() : ConvertStringToIntList(dataReader.GetValue(13).ToString());
                    recipe.FeatureIds = dataReader[14] == DBNull.Value ? new List<int>() : ConvertStringToIntList(dataReader.GetValue(14).ToString());
                    recipe.FeatureCategoryIds = dataReader[15] == DBNull.Value ? new List<int>() : ConvertStringToIntList(dataReader.GetValue(15).ToString());

                    recipeList.Add(recipe);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                
               
            }
            dataReader.Close();
            return recipeList;
        }

        private List<int> ConvertStringToIntList(string text)
        {
            if(!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text) || text == "NULL"))
            {
                return text.Replace(",NULL", "").Trim().Split(',').Select(x => Convert.ToInt32(x)).ToList();
            }

            return new List<int>();
            
        }
    }
}
