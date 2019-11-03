using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Model.DataAccess;
using Model.Domain;
using RecipeSearch.Models.Feature;

namespace RecipeSearch.Services
{
    public class FeatureService
    {
        private readonly IFeatureDao _featureDao;

        public FeatureService(): this(new FeatureDao())
        {

        }

        public FeatureService(FeatureDao featureDao)
        {
            _featureDao = featureDao;
        }

        public async Task<List<FeatureCategoriesModel>> SelectFeatureCategories()
        {
            return _featureDao.SelectFeatureCategories().Select(x => new FeatureCategoriesModel()
            {
                Id = x.FeatureCategoryId,
                Name = x.FeatureCategoryName

            }).OrderBy(y => y.Id).ToList();
        }

        public async Task<List<FeatureModel>> SelectFeatures()
        {
            return _featureDao.SelectFeatures().Select(x => new FeatureModel()
            {
                Id = x.FeatureId,
                Name = x.FeatureName,
                CategoryId = x.FeatureCategoryId
            }).OrderBy(y => y.Id).ToList();
        }

        public async Task<List<RecipeFeatureXrefModel>> SelectRecipeFeatureXref()
        {
            return _featureDao.SelectRecipeFeatureXref().Select(x => new RecipeFeatureXrefModel()
            {
                RecipeId = x.RecipeId,
                FeatureId = x.FeatureId
            }).OrderBy(y => y.RecipeId).ToList();
        }

    }
}