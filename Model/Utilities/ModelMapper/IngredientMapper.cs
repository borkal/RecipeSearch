using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;

namespace Model.Utilities.ModelMapper
{
    public class IngredientMapper
    {
        public List<Ingredient> SelectAllIngredientsByRecipeIdMapper(OdbcDataReader dataReader)
        {
            var ingredientList = new List<Ingredient>();
            while (dataReader.Read())
            {
                //TODO: find a way to read multiple records into object list
            }

            return ingredientList;
        }
    }
}
