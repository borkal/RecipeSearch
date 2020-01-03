using RecipeSearch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RecipeSearch.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FeatureController : ApiController
    {
        private readonly FeatureService _featureService;

        public FeatureController() : this(new FeatureService())
        {

        }

        public FeatureController(FeatureService featureService)
        {
            _featureService = featureService;
        }

        [Route("feature/featureCategories")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectFeatureCategories()
        {
            try
            {
                var featureCategoriesList = await _featureService.SelectFeatureCategories();
                return Ok(new
                {
                    featureCategories = featureCategoriesList
                });
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("feature/features")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectFeatures()
        {
            try
            {
                var featuresList = await _featureService.SelectFeatures();
                return Ok(new
                {
                    features = featuresList
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("feature/recipeFeatures")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectRecipeFeatureXref()
        {
            try
            {
                var recipeFeaturesList = await _featureService.SelectRecipeFeatureXref();
                return Ok(new
                {
                    recipeFeatures = recipeFeaturesList
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
