using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Model.DataAccess;
using Model.Domain;
using RecipeSearch.Services;

namespace RecipeSearch.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DishController : ApiController
    {
        private readonly DishService _dishService;

        public DishController() : this(new DishService())
        {
            
        }

        public DishController(DishService dishService)
        {
            _dishService = dishService;
        }


        [Route("dish/dishes")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectDishes()
        {
            try
            {
                var dishList = await _dishService.SelectDishes();
                return Ok(new
                {
                    dishes = dishList
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            
        }
        
        [Route("dish/dishSubCategories")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectDishSubCategories()
        {
            try
            {
                var dishSubCategoryList = await _dishService.SelectDishSubCategories();
                return Ok(new
                {
                    dishSubCategories = dishSubCategoryList
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        [Route("dish/dishMainCategories")]
        [HttpGet]
        public async Task<IHttpActionResult> SelectDishMainCategories()
        {
            try
            {
                var dishMainCategoryList = await _dishService.SelectDishMainCategories();
                return Ok(new
                {
                    dishCategories = dishMainCategoryList
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }
    }
}
