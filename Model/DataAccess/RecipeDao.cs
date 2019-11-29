using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model.Domain;
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
            return _recipeMapper.SelectAlLRecipesBySearchText(dbReader);
        }

        public Recipe SelectRecipeByRecipeId(int recipeId)
        {
            var query = _recipeQuery.SelectRecipeByRecipeId(recipeId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectRecipeByRecipeIdMapper(dbReader);
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
    }
}
