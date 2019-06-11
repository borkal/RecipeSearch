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
        Dictionary<int, string> SearchRecipeIdsBasedOnSearchText(string searchText);
        Recipe SelectRecipeByRecipeId(int recipeId);
        string SelectBlogNameByRecipeId(int recipeId);
        List<Recipe> SelectAlLRecipesBySearchText(string searchText);
    }
}
