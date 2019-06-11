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
        public bool Process { get; set; } = true;

        public FantazjeMagdyKParser(string url)
        {
            RecipeToProcessUrl = url;
            RecipeWebDocument = new HtmlWeb();
            try
            {
                RecipeHtmlDocument = RecipeWebDocument.Load(RecipeToProcessUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Process = false;

            }
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

            //pattern #1
            var ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"//*[@class='post-body entry-content']//text()[preceding-sibling::span[text() ='wykonanie:' or text() ='wykonanie' or text() ='wykonanie:<br>'] and following-sibling::a]");

            if (ingredientDescriptionElements == null)
            {
                //pattern #2
                ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"//*[@class='post-body entry-content']//*[preceding-sibling::div/b[text()='wykonanie:'] and following-sibling::div[text()='\nSmacznego!!']]/text()");
                if (ingredientDescriptionElements == null)
                {
                    //pattern #2 - probably better? it may find more descriptions than patterh #2 but data could be wrong
                    ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"//*[@class='post-body entry-content']//*[preceding-sibling::div/b[starts-with(.,'wykonanie')] and following-sibling::div[text()='\nSmacznego!!']]/text()");
                    if (ingredientDescriptionElements == null)
                    {
                        //patter#3
                        ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"//*[@class='post-body entry-content']//text()[preceding-sibling::span[text() ='wykonanie:' or text() ='wykonanie' or text() ='wykonanie:<br>']]");
                    }

                    //pattern#4
                    ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"//*[@class='post-body entry-content']//*[preceding-sibling::p/b[text() ='wykonanie:' or text() ='wykonanie' or text() ='wykonanie:<br>'] and following-sibling::a]/text()");
                    if (ingredientDescriptionElements == null)
                    {
                        //test paterns that went wrong
                        ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyPattern}");
                        ingredientDescriptionElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyPattern}//*[contains(text(),'wykonanie:')]"); //[preceding-sibling::*[text() ='wykonanie:' or text() ='wykonanie' or text() ='wykonanie:<br>']]");
                    }
                }

                if (ingredientDescriptionElements == null)
                {
                    ErrorRecipesList.Add(RecipeId, RecipeToProcessUrl);
                    process = false;
                }
            }

            if (process)
            {
                foreach (var element in ingredientDescriptionElements.Where(x => x.InnerHtml != "\n" || x.InnerHtml.Length != 0))
                {
                    descriptionList.Add(element.InnerText.TrimStart(':')
                                                         .Replace("\n", "")
                                                         .Replace("&#189;", "1/2"));
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
                httpResponse.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return process;

        }

        string IParser.GetImage()
        {
            throw new NotImplementedException();
        }
    }
}
