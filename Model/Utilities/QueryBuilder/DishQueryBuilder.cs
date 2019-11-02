using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.QueryBuilder
{
    public class DishQueryBuilder
    {
        public string SelectDishes()
        {
            return "SELECT " +
                   "D.id, " +
                   "D.name, " +
                   "D.category_id " +
                   "FROM dish D";
        }

        public string SelectDishSubCategories()
        {
            return "SELECT " +
                   "DC.id, " +
                   "DC.name, " +
                   "DC.parent_id " +
                   "FROM dishcategory DC " +
                   "WHERE DC.parent_id IS NOT NULL";
        }

        public string SelectDishMainCategories()
        {
            return "SELECT " +
                   "DM.id, " +
                   "DM.name " +
                   "FROM dishcategory DS " +
                   "JOIN dishCategory DM on DS.parent_id = DM.id " +
                   "GROUP BY DM.id";

        }
            

    }
}
