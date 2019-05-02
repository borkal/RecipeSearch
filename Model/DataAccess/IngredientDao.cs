using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;
using Model.Utilities.Odbc;

namespace Model.DataAccess
{
    public class IngredientDao : IIngredientDao
    {
        private readonly OdbcManager _odbcManager;

        public IngredientDao() : this(new OdbcManager())
        {

        }

        public IngredientDao(OdbcManager odbcManager)
        {
            _odbcManager = odbcManager;
        }

        public List<Ingredient> SelectAllIngredientByRecipeId(int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
