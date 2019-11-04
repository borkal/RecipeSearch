using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;

namespace Model.DataAccess
{
    public interface IRecipeDao
    {
        Recipe SelectRecipeByRecipeId(int recipeId);
        List<Recipe> SelectAlLRecipesBySearchText(string searchText, int[] dishIds, int[] dishSubCategoryIds, int[] dishMainCategoryIds, int[] ingredientIds, int[] ingredientCategoryIds, int[]featureIds, int[]deatureCategoryIds, bool? citrus, bool? nut, bool? sugar, bool? mushroom, bool? gluten, bool? cowMilk, bool? wheat, bool? egg, bool? vegetarian, int count);
    }
}
