﻿using System;
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
        List<Recipe> SelectAlLRecipesBySearchText(SearchRecipe searchRecipe);
        List<string> SelectRecipeIngredientsFromDatabase(int recipeId);
        List<string> SelectRecipeDescriptionFromDatabase(int recipeId);
        Recipe SelectRandomRecipe();
    }
}
