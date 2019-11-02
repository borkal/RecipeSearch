using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;

namespace Model.DataAccess
{
    public interface IIngredientDao
    {
        List<Ingredient> SelectIngredients();
        List<IngredientCategory> SelectIngredientCategories();
        List<IngredientCategoryXref> SelectIngriedentCategoiresXref();
    }
}
