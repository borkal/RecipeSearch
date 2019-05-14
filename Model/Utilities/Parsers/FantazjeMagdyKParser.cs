using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Model.Utilities.Parsers
{
    public class FantazjeMagdyKParser : IParser
    {
        private static string DivBodyPattern = "//*[@class='post-body entry-content']";
        public Dictionary<int, string> ErrorRecipesList = new Dictionary<int, string>();
        public int RecipeId { get; set; }
        private HtmlDocument RecipeHtmlDocument { get; }
        private string RecipeToProcessUrl { get; }
        private HtmlWeb RecipeWebDocument { get; }

        public FantazjeMagdyKParser(string url)
        {
            RecipeToProcessUrl = url;
            RecipeWebDocument = new HtmlWeb();
            RecipeHtmlDocument = RecipeWebDocument.Load(RecipeToProcessUrl);
        }

        public void GetImage()
        {

        }
        public void GetTitle()
        {

        }

        public List<string> GetDescription()
        {
            var descriptionList = new List<string>();
            bool process = true;

            var ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyPattern}//text()[preceding-sibling::span[text() ='wykonanie:' or text() ='wykonanie' or text() ='wykonanie:<br>'] and following-sibling::a]");

            if (ingredientDescriptionElements == null)
            {
                ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyPattern}//*[preceding-sibling::div/b[text()='wykonanie:'] and following-sibling::div[text()='\nSmacznego!!']]/text()");
                if (ingredientDescriptionElements == null)
                {
                    ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyPattern}//*[preceding-sibling::div/b[starts-with(.,'wykonanie')] and following-sibling::div[text()='\nSmacznego!!']]/text()");
                }

                if (ingredientDescriptionElements == null)
                {
                    ErrorRecipesList.Add(RecipeId, RecipeToProcessUrl);
                    process = false;
                }
            }

            if (process)
            {
                foreach (var element in ingredientDescriptionElements.Where(x => x.InnerHtml != "\n"))
                {
                    descriptionList.Add(element.InnerText);
                }
            }


            return descriptionList;
        }


        public List<string> GetIngredients()
        {
            var ingredientList = new List<string>();
            bool process = true;

            var ingredientLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyPattern}//ul/li");

            if (ingredientLiElements == null)
            {
                ingredientLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyPattern}/text()[preceding-sibling::span[text() ='składniki:'] and following-sibling::span]");
                if (ingredientLiElements == null)
                {
                    ErrorRecipesList.Add(RecipeId, RecipeToProcessUrl);
                    process = false;
                }
            }

            if (process)
            {
                foreach (var element in ingredientLiElements.Where(x => x.InnerHtml != "\n"))
                {
                    ingredientList.Add(element.InnerText.Replace("&#189;", "1/2")
                                                        .Replace("&#8211;", "-")
                                                        .Replace("\n", ""));
                }
            }

            return ingredientList;
        }

        public bool CheckIfImageExist(string imgUrl)
        {
            bool process = false;
            var httpRequest = (HttpWebRequest)WebRequest.Create(imgUrl);
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                process = httpResponse.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return process;

        }


    }
}
