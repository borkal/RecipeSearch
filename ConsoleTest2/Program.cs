using Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enums;
using Model.Utilities.Parsers;
//using RecipeSearch.RecipeService;

namespace ConsoleTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            var recipeDao = new RecipeDao();

            var results = recipeDao.SelectRecipesByBlogId((int) Blogs.KwestiaSmaku);
            foreach (var recipe in results)
            {
                int orderNumber = 0;
                var blog = new KwestiaSmakuParser(recipe.RecipeUrl);
                var ingredients = blog.GetIngredients().Where(x => !string.IsNullOrEmpty(x)).ToList();
                var description = blog.GetDescription().Where(x => !string.IsNullOrEmpty(x)).ToList();
                //description.RemoveAt(12);
                //description.RemoveRange(0,1);

                ingredients.ForEach(x => recipeDao.InsertRecipeIngredient(recipe.RecipeId, x));
                description.ForEach(x => recipeDao.InsertRecipeDescription(recipe.RecipeId, x, ++orderNumber));

            }

            Console.ReadKey();

        }
    }
}
