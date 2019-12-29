using Model.Domain;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.ModelMapper
{
    public class FeatureMapper
    {
        public List<Feature> SelectRecipeFeatureByRecipeIdMapper(OdbcDataReader dataReader)
        {
            var featureList = new List<Feature>();
            while (dataReader.Read())
            {
                Feature featureToList = new Feature();
                featureToList.FeatureId = dataReader.GetInt32(0);
                featureToList.FeatureName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);

                featureList.Add(featureToList);
            }
            dataReader.Close();
            return featureList;
        }

        public List<FeatureCategory> SelectFeatureCategories(OdbcDataReader dataReader)
        {
            var FeatureCategoryList = new List<FeatureCategory>();
            while (dataReader.Read())
            {
                var featureCategoryToList = new FeatureCategory();
                featureCategoryToList.FeatureCategoryId = dataReader.GetInt32(0);
                featureCategoryToList.FeatureCategoryName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);

                FeatureCategoryList.Add(featureCategoryToList);
            }
            dataReader.Close();
            return FeatureCategoryList;
        }

        public List<Feature> SelectFeatures(OdbcDataReader dataReader)
        {
            var FeaturesList = new List<Feature>();
            while (dataReader.Read())
            {
                var featureToList = new Feature();
                featureToList.FeatureId = dataReader.GetInt32(0);
                featureToList.FeatureName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                featureToList.FeatureCategoryId = dataReader.GetInt32(2);

                FeaturesList.Add(featureToList);
            }
            dataReader.Close();
            return FeaturesList;
        }

        public List<RecipeFeatureXref> SelectRecipeFeatureXref(OdbcDataReader dataReader)
        {
            var RecipeFeatureList = new List<RecipeFeatureXref>();
            while(dataReader.Read())
            {
                var recipeFeatureToList = new RecipeFeatureXref();
                recipeFeatureToList.RecipeId = dataReader.GetInt32(0);
                recipeFeatureToList.FeatureId = dataReader.GetInt32(1);

                RecipeFeatureList.Add(recipeFeatureToList);
            }
            dataReader.Close();
            return RecipeFeatureList;
        }


    }
}
