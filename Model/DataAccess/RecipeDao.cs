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

        public List<Recipe> SelectAlLRecipesBySearchText(string searchText, int[] dishIds, int[] dishSubCategoryIds, int[] dishMainCategoryIds, int[] ingredientIds, int[] ingredientCategoryIds, int[] featureIds, int[] featureCategoryIds, bool? citrus, bool? nut, bool? sugar, bool? mushroom, bool? gluten, bool? cowMilk, bool? wheat, bool? egg, bool? vegetarian, int count)
        {
            var query = _recipeQuery.SelectAlLRecipesBySearchText(searchText, dishIds, dishSubCategoryIds, dishMainCategoryIds, ingredientIds, ingredientCategoryIds, featureIds, featureCategoryIds, citrus, nut, sugar, mushroom, gluten, cowMilk, wheat, egg, vegetarian, count);
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
