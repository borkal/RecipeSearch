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
        public List<int> SearchRecipeIdsBasedOnSearchText(string searchText)
        {
            var query = _recipeQuery.SearchRecipeIdsBasedOnSearchText(searchText);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SearchRecipeIdsBasedOnSearchTextMapper(dbReader);
        }

        public Recipe SelectRecipeByRecipeId(int recipeId)
        {
            var query = _recipeQuery.SelectRecipeByRecipeId(recipeId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectRecipeByRecipeIdMapper(dbReader);
        }

        public List<Recipe> SelectRecipesByBlogName(string blogName)
        {
            var query = _recipeQuery.SelectRecipesByBlogName(blogName);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _recipeMapper.SelectRecipesByBlogNameMapper(dbReader);
        }


        //olej ta metode narazie
        public Dictionary<int, string> SelecAllRecipeNamesAndIds(string searchText)
        {
            var query = _recipeQuery.SelecAllRecipeNamesAndIds();
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            var mapper = _recipeMapper.SelecAllRecipeNamesAndIds(dbReader);

            var reg = new Regex($".*{searchText}.*", RegexOptions.IgnoreCase);
            var test = mapper.Where(x => reg.IsMatch(x.Value));

            return (Dictionary<int, string>)mapper.Where(x => reg.IsMatch(x.Value));
        }
    }
}
