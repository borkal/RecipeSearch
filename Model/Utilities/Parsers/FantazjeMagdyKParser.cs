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
        //public Dictionary<int, string> ErrorRecipesList = new Dictionary<int, string>();s
        private HtmlDocument RecipeHtmlDocument { get; }
        private string RecipeToProcessUrl { get; }
        private HtmlWeb RecipeWebDocument { get; }
        public bool Process { get; set; } = true;
        public List<string> XpathDescriptionPatternList { get; set; } = new List<string>();
        public List<string> XpathIngredientsPatternList { get; set; } = new List<string>();
        public FantazjeMagdyKParser(string url)
        {
            RecipeToProcessUrl = url;
            RecipeWebDocument = new HtmlWeb();
            try
            {
                RecipeHtmlDocument = RecipeWebDocument.Load(RecipeToProcessUrl);//,"GET", new WebProxy("qgproxy.qg.com"), new NetworkCredential());
                InitXpathPatterns();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Process = false;

            }
        }

        public string GetImage()
        {
            return "";
        }
        public void GetTitle()
        {

        }

        public List<string> GetDescription()
        {
            var descriptionList = new List<string>();

            foreach (var pattern in XpathDescriptionPatternList)
            {
                var page = RecipeHtmlDocument.DocumentNode.SelectNodes(pattern);
                if (page != null)
                {
                    foreach (var element in page.Where(x => x.InnerHtml != "\n" || x.InnerHtml.Length != 0))
                    {
                        descriptionList.Add(element.InnerText
                            .TrimStart(':')
                            .Replace("\n", "")
                            .Replace("&#189;", "1/2")
                            .Replace("\r\n1", ""));
                    }

                    break;
                }
            }

            return descriptionList;
        }

        public List<string> GetIngredients()
        {
            var ingredientsList = new List<string>();

            foreach (var pattern in XpathIngredientsPatternList)
            {
                var page = RecipeHtmlDocument.DocumentNode.SelectNodes(pattern);
                if (page != null)
                {
                    foreach (var element in page.Where(x => x.InnerHtml != "\n" || x.InnerHtml.Length != 0))
                    {
                        ingredientsList.Add(element.InnerText
                            .TrimStart(':')
                            .Replace("\n", "")
                            .Replace("&#189;", "1/2")
                            .Replace("\r\n1", "")
                            .Replace("&#8211;", "-"));
                    }
                }
            }
            return ingredientsList;
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

        public void InitXpathPatterns()
        {
            XpathDescriptionPatternList.Add($"{DivBodyPattern}//text()[preceding-sibling::span[text() ='wykonanie:' or text() ='wykonanie' or text() ='wykonanie:<br>'] and following-sibling::a]");
            XpathDescriptionPatternList.Add($"{DivBodyPattern}//*[preceding-sibling::div/b[text()='wykonanie:'] and following-sibling::div[text()='\nSmacznego!!']]/text()");
            XpathDescriptionPatternList.Add($"{DivBodyPattern}//*[preceding-sibling::div/b[starts-with(.,'wykonanie')] and following-sibling::div[text()='\nSmacznego!!']]/text()");
            XpathDescriptionPatternList.Add($"{DivBodyPattern}//text()[preceding-sibling::span[text() ='wykonanie:' or text() ='wykonanie' or text() ='wykonanie:<br>']]");
            XpathDescriptionPatternList.Add($"{DivBodyPattern}//*[preceding-sibling::p/b[text() ='wykonanie:' or text() ='wykonanie' or text() ='wykonanie:<br>'] and following-sibling::a]/text()");
            
            XpathIngredientsPatternList.Add($"{DivBodyPattern}//ul/li");
            XpathIngredientsPatternList.Add($"{DivBodyPattern}/text()[preceding-sibling::span[text() ='składniki:'] and following-sibling::span]");
        }
    }
}
