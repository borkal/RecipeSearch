using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.QueryBuilder
{
    public class FeatureQueryBuilder
    {
        public string SelectRecipeFeatureByRecipeId(int recipeId)
        {
            var query = "SELECT " +
                        "F.id," +
                        "F.name " +
                        "FROM feature F " +
                        "LEFT JOIN recipe_feature RF ON F.id = RF.feature_id " +
                        "LEFT JOIN recipe R ON RF.recipe_id = R.id " +
                        $"WHERE R.Id = {recipeId} ";

            return query;
        }

        public string SelectFeatureCategories()
        {
            return "SELECT " +
                "FC.id, " +
                "FC.name " +
                "FROM featurecategory FC ";
        }

        public string SelectFeatures()
        {
            return "SELECT " +
                "F.id, " +
                "F.name, " +
                "F.category_id " +
                "FROM feature F ";
        }

        public string SelectRecipeFeatureXref()
        {
            return "SELECT " +
                "RF.recipe_id, " +
                "RF.feature_id " +
                "FROM recipe_feature RF";
        }

    }
}
