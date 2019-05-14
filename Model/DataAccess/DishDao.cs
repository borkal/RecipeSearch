using Model.Domain;
using Model.Utilities.ModelMapper;
using Model.Utilities.Odbc;
using Model.Utilities.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public class DishDao : IDishDao
    {
        private readonly OdbcManager _odbcManager;
        private readonly DishQueryBuilder _dishQuery;
        private readonly DishMapper _dishMapper;

        public DishDao() : this(new OdbcManager(), new DishQueryBuilder(), new DishMapper())
        {

        }

        public DishDao(OdbcManager odbcManager, DishQueryBuilder dishQueryBuilder, DishMapper dishMapper)
        {
            _odbcManager = odbcManager;
            _dishQuery = dishQueryBuilder;
            _dishMapper = dishMapper;
        }
        public List<Dish> SelectRecipeDishByRecipeId(int recipeId)
        {
            var query = _dishQuery.SelectRecipeDishByRecipeId(recipeId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _dishMapper.SelectRecipeDishByRecipeIdMapper(dbReader);
        }
    }
}
