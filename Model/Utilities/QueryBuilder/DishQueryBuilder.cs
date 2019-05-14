using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.QueryBuilder
{
    public class DishQueryBuilder
    {
        public string SelectRecipeDishByRecipeId(int recipeId)
        {
            var query = "SELECT " +
                        "D.id," +
                        "D.name " +
                        "FROM dish D " +
                        "LEFT JOIN recipe R ON D.id = R.dish_id " +
                        $"WHERE R.id = {recipeId}";
            //jak podejsc do kwestii dishcategory???
            //tak samo featurecategory???

            return query;
        }

    }
}
