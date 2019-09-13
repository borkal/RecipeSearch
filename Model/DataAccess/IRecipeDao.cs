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
        List<Recipe> SelectAlLRecipesBySearchText(string searchText);
    }
}
