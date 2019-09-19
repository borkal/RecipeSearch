using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.Parsers
{
    public class KwestiaSmakuParser : IParser
    {
        private static readonly string DivBodyIngredientsPattern = "//*[@class='group-skladniki field-group-div']";
        private static readonly string DivBodyDescriptionPattern = "//*[@class='group-przepis field-group-div']"; //group-przepis field-group-div
        private static readonly string DivBodyImagePattern = "//*[@class='field field-name-zdjecie-z-linikem-do-bloga field-type-ds field-label-hidden']";
        public Dictionary<int, string> ErrorRecipesList = new Dictionary<int, string>();
        public int RecipeId { get; set; }
        private HtmlDocument RecipeHtmlDocument { get; }
        private string RecipeToProcessUrl { get; }
        private HtmlWeb RecipeWebDocument { get; }
        public bool Process { get; set; } = true;
        public List<string> XpathDescriptionPatternList { get; set; } = new List<string>();
        public List<string> XpathIngredientsPatternList { get; set; } = new List<string>();

        public KwestiaSmakuParser(string url)
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

        public List<string> GetDescription()
        {
            var descriptionList = new List<string>();
            bool process = true;

            var descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyDescriptionPattern}//ul/li");

            if (descriptionLiElements == null)
            {
                descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyDescriptionPattern}//text()[preceding-sibling::span[text() ='Przygotowanie'] and following-sibling::a]");
                if(descriptionLiElements == null)
                {
                    descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyDescriptionPattern}//p");
                }


                if (descriptionLiElements == null)
                {
                    ErrorRecipesList.Add(RecipeId, RecipeToProcessUrl);
                    process = false;
                }
            }

            if (process)
            {
                foreach (var element in descriptionLiElements.Where(x => x.InnerHtml != "\n"))
                {
                    descriptionList.Add(element.InnerText.Replace("&nbsp;", " ")
                                                         .Replace("\n\t\t", ""));
                }
            }


            return descriptionList;
        }

        public string GetImage()
        {

            var imagesLiElements = RecipeHtmlDocument.DocumentNode.SelectSingleNode($"{DivBodyImagePattern}//a/img").Attributes["src"].Value;

            if (imagesLiElements == null)
            {
                ErrorRecipesList.Add(RecipeId, RecipeToProcessUrl);
            }

            return imagesLiElements;
        }

        public List<string> GetIngredients()
        {
            var ingredientList = new List<string>();
            bool process = true;

            var ingredientLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyIngredientsPattern}//ul/li");

            if (ingredientLiElements == null)
            {
                ingredientLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyIngredientsPattern}/text()[preceding-sibling::span[text() ='składniki:'] and following-sibling::span]");
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
                                                        .Replace("\n", "")
                                                        .Replace("\t\t", ""));
                }
            }

            return ingredientList;
        }
        public void GetTitle()
        {
            throw new NotImplementedException();
        }

        public bool CheckIfImageExist(string imgUrl) //jakies problemy, pytac Borysa ocb.
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
            XpathDescriptionPatternList.Add($"{DivBodyDescriptionPattern}//ul/li");
            XpathDescriptionPatternList.Add($"{DivBodyDescriptionPattern}//text()[preceding-sibling::span[text() ='Przygotowanie'] and following-sibling::a]");
            XpathDescriptionPatternList.Add($"{DivBodyDescriptionPattern}//p");

            XpathIngredientsPatternList.Add($"{DivBodyIngredientsPattern}//ul/li");
            XpathIngredientsPatternList.Add($"{DivBodyIngredientsPattern}/text()[preceding-sibling::span[text() ='składniki:'] and following-sibling::span]");
        }
    }
}