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

            int i = 1;
            int added = 0;
            int notadded = 0;
            var results = recipeDao.SelectRecipesByBlogId((int)Blogs.MojeDietetyczneFanaberie);
            foreach (var recipe in results)
            {
                int orderNumber = 0;
                var blog = new MojeDietetyczneFanaberieParser(recipe.RecipeUrl);
                var ingredients = blog.GetIngredients().Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Replace("'", "")).ToList();
                var description = blog.GetDescription().Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Replace("'", "")).ToList();

                var text = recipe.RecipeId;
                Console.WriteLine(text);
                Console.WriteLine();

                //ingredients.ForEach(Console.WriteLine);

                int linecounteringredients = 1;
                foreach (var item in ingredients)
                {
                    Console.WriteLine(linecounteringredients + " " + item);
                    linecounteringredients++;
                }

                Console.WriteLine();

                //description.ForEach(Console.WriteLine);

                int linecounterdescription = 1;
                foreach (var item in description)
                {
                    Console.WriteLine(linecounterdescription + " " + item);
                    linecounterdescription++;
                }

                Console.WriteLine("Dodać przepis?");
                var decision = Console.ReadLine();

                //description.RemoveAt(12);
                // description.RemoveRange(5, 35);
                //ingredients.RemoveRange(9, 9);

                if (decision == "Y")
                {
                    added++;
                    System.IO.File.AppendAllText(@"C:\Users\Dawid\Desktop\WriteText.txt", i + ". " + text.ToString() + " YES, ADDED: " + added + Environment.NewLine); i++;
                    ingredients.ForEach(x => recipeDao.InsertRecipeIngredient(recipe.RecipeId, x));
                    description.ForEach(x => recipeDao.InsertRecipeDescription(recipe.RecipeId, x, ++orderNumber));
                }
                else
                {
                    notadded++;
                    System.IO.File.AppendAllText(@"C:\Users\Dawid\Desktop\WriteText.txt", i + ". " + text.ToString() + " NO, NOT ADDED: " + notadded + Environment.NewLine); i++;
                    continue;
                }
            }

            Console.ReadKey();

        }
    }
}
