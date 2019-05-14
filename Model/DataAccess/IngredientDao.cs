using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;
using Model.Utilities.ModelMapper;
using Model.Utilities.Odbc;
using Model.Utilities.QueryBuilder;

namespace Model.DataAccess
{
    public class IngredientDao : IIngredientDao
    {
        private readonly OdbcManager _odbcManager;
        private readonly IngredientQueryBuilder _ingredientQuery;
        private readonly IngredientMapper _ingredientMapper;

        public IngredientDao() : this(new OdbcManager(), new IngredientQueryBuilder(), new IngredientMapper())
        {

        }

        public IngredientDao(OdbcManager odbcManager, IngredientQueryBuilder ingredientQueryBuilder, IngredientMapper ingredientMapper)
        {
            _odbcManager = odbcManager;
            _ingredientQuery = ingredientQueryBuilder;
            _ingredientMapper = ingredientMapper;

        }

        public List<Ingredient> SelectAllIngredientsByRecipeId(int recipeId)
        {
            var query = _ingredientQuery.SelectAllIngredientsByRecipeId(recipeId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _ingredientMapper.SelectAllIngredientsByRecipeIdMapper(dbReader);
        }
    }
}
