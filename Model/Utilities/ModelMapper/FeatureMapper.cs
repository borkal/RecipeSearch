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

            return featureList;
        }
    }
}
