using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Model.Utilities.Parsers
{
    public class FantazjeMagdyKParser : IParser
    {
        private HtmlDocument RecipeHtmlDocument { get; set; }
        private string RecipeToProcessUrl { get; set; }
        private HtmlWeb RecipeWebDocument { get; set; }

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

        public void GetDescription()
        {
            var documentNode = RecipeHtmlDocument.DocumentNode.SelectNodes("//div/*");
            //var documentNode1 = RecipeHtmlDocument.DocumentNode.SelectNodes("//*[@class='post-body entry-content']");
        }

        public string GetIngredients(string rec)
        {
            var recipeUrl = rec;
            var ingredientList = new List<string>();
            var errorlist = "";
            var ingredientLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes("//*[@class='post-body entry-content']//ul/li");
            if (ingredientLiElements == null)
            {
                errorlist += recipeUrl;
            }
            //foreach (var element in ingredientLiElements.Elements())
            //{
            //    ingredientList.Add(element.InnerText);
            //}

            return errorlist;
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
