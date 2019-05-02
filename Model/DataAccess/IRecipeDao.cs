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
        List<int> SearchRecipeIdsBasedOnSearchText(string searchText);
        Recipe SelectRecipeByRecipeId(int recipeId);
    }
}
