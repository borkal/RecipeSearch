﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model.Domain;
using Model.Domain.Recipe;
using Model.Utilities.ModelMapper;
using Model.Utilities.Odbc;
using Model.Utilities.QueryBuilder;


namespace Model.DataAccess
{
    public class RecipeDao : IRecipeDao
    {
        private readonly OdbcManager _odbcManager;
        private readonly RecipeQueryBuilder _recipeQuery;
        private readonly RecipeMapper _recipeMapper;

        public RecipeDao() : this(new OdbcManager(), new RecipeQueryBuilder(), new RecipeMapper())
        {

        }
        public RecipeDao(OdbcManager odbcManager, RecipeQueryBuilder recipeQueryBuilder, RecipeMapper recipeMapper)
        {
            _odbcManager = odbcManager;
            _recipeQuery = recipeQueryBuilder;
            _recipeMapper = recipeMapper;
        }

        public List<string> SelectRecipeIngredientsFromDatabase(int recipeId)
        {
            var query = _recipeQuery.SelectRecipeIngredientsFromDatabase(recipeId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectRecipeIngredientsFromDatabaseMapper(dbReader);
        }
        public List<string> SelectRecipeDescriptionFromDatabase(int recipeId)
        {
            var query = _recipeQuery.SelectRecipeDescriptionFromDatabase(recipeId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectRecipeDescriptionFromDatabaseMapper(dbReader);
        }

        public List<Recipe> SelectAlLRecipesBySearchText(SearchRecipe searchRecipe)
        {
            var query = _recipeQuery.SelectAlLRecipesBySearchText(searchRecipe);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            var mapper = _recipeMapper.SelectAlLRecipesBySearchText(dbReader);
            
            return mapper;
        }
        public List<Recipe> SelectAlLRecipesBySearchTextPaged(SearchRecipe2 searchRecipe)
        {
            var query = _recipeQuery.SelectAlLRecipesBySearchTextPaged(searchRecipe);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            var mapper = _recipeMapper.SelectAlLRecipesBySearchTextPaged(dbReader);
            
            return mapper;
        }

        public Recipe SelectRecipeByRecipeId(int recipeId)
        {
            var query = _recipeQuery.SelectRecipeByRecipeId(recipeId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            var mapper = _recipeMapper.SelectRecipeByRecipeIdMapper(dbReader);
            var rates = SelectRecipeRates(new List<int> {mapper.RecipeId});

            mapper.Rates = rates.Where(x => x.recipeId == mapper.RecipeId).Select(y => y.recipeRate.ToString()).ToList();
            mapper.CalculateTotalRecipeRate();

            return mapper;
        }

        public List<RecipeRate> SelectRecipeRates(List<int> recipeIds)
        {
            var query = _recipeQuery.SelectRecipesRates(recipeIds);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            var mapper = _recipeMapper.SelectRecipesRates(dbReader);
            return mapper;
        }

        public List<Recipe> SelectFavRecipesByUser(string username)
        {
            var query = _recipeQuery.SelectFavRecipesByUser(username);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectFavRecipesByUserMapper(dbReader);
        }

        public void InsertFavRecipeOfUserIntoDatabase(int recipeId, string username)
        {
            var query = _recipeQuery.InsertFavRecipeOfUserIntoDatabase(recipeId, username);
            _odbcManager.ExecuteUpdateQuery(query);
        }

        public DayRecipe SelectRecipeOfTheDayRowFromDatabase()
        {
            var query = _recipeQuery.SelectRecipeOfTheDayRowFromDatabase();
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectRecipeOfTheDayRowFromDatabaseMapper(dbReader);
        }

        public void InsertRecipeOfTheDayRowToDatabase(string date, int recipeId)
        {
            var query = _recipeQuery.InsertRecipeOfTheDayRowToDatabase(date, recipeId);
            _odbcManager.ExecuteUpdateQuery(query);
        }

        public List<Recipe> SelectRecipesByBlogId(int blogId)
        {
            var query = _recipeQuery.SelectRecipesByBlogId(blogId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectRecipesByBlogIdMapper(dbReader);
        }

        public void InsertRecipeIngredient(int recipeId, string ingredientText)
        {
            var query = _recipeQuery.InsertRecipeIngredient(recipeId, ingredientText);
            _odbcManager.ExecuteUpdateQuery(query);
        }

        public void InsertRecipeDescription(int recipeId, string descriptionText, int orderId)
        {
            var query = _recipeQuery.InsertRecipeDescription(recipeId, descriptionText, orderId);
            _odbcManager.ExecuteUpdateQuery(query);
        }

        public void InsertRecipeRateIntoDatabase(int recipeId, int rate, string username)
        {
            var query = _recipeQuery.InsertRecipeRateIntoDatabase(recipeId, rate, username);
            _odbcManager.ExecuteUpdateQuery(query);
        }

        public RecipeRate SelectUserRateDataFromRateTable(int id, string username)
        {
            var query = _recipeQuery.SelectUserRateDataFromRateTable(id, username);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectUserRateDataFromRateTableMapper(dbReader);
        }

        public List<int> SelectRandomRecipeIds(int count)
        {
            var query = _recipeQuery.SelectRandomRecipeIds(count);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectRandomRecipeIds(dbReader);

        }
        
    }
}
