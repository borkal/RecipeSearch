using Model.Domain;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.ModelMapper
{
    public class DishMapper
    {
        public List<Dish> SelectRecipeDishByRecipeIdMapper(OdbcDataReader dataReader)
        {
            var dishList = new List<Dish>();
            while (dataReader.Read())
            {
                Dish dishToList = new Dish();
                dishToList.DishId = dataReader.GetInt32(0);
                dishToList.DishName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);

                dishList.Add(dishToList);
            }

            return dishList;
        }
    }
}
