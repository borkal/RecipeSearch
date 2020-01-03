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
    public class FeatureDao : IFeatureDao
    {
        private readonly OdbcManager _odbcManager;
        private readonly FeatureQueryBuilder _featureQuery;
        private readonly FeatureMapper _featureMapper;

        public FeatureDao() : this(new OdbcManager(), new FeatureQueryBuilder(), new FeatureMapper())
        {

        }
        public FeatureDao(OdbcManager odbcManager, FeatureQueryBuilder featureQueryBuilder, FeatureMapper featureMapper)
        {
            _odbcManager = odbcManager;
            _featureQuery = featureQueryBuilder;
            _featureMapper = featureMapper;
        }
        public List<Feature> SelectRecipeFeatureByRecipeId(int recipeId)
        {
            var query = _featureQuery.SelectRecipeFeatureByRecipeId(recipeId);
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _featureMapper.SelectRecipeFeatureByRecipeIdMapper(dbReader);
        }

        public List<FeatureCategory> SelectFeatureCategories()
        {
            var query = _featureQuery.SelectFeatureCategories();
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _featureMapper.SelectFeatureCategories(dbReader);
        }

        public List<Feature> SelectFeatures()
        {
            var query = _featureQuery.SelectFeatures();
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _featureMapper.SelectFeatures(dbReader);
        }

        public List<RecipeFeatureXref> SelectRecipeFeatureXref()
        {
            var query = _featureQuery.SelectRecipeFeatureXref();
            var dbReader = _odbcManager.ExecuteReadQuery(query);
            return _featureMapper.SelectRecipeFeatureXref(dbReader);
        }
    }
}
