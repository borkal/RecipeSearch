﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.Parsers
{
    public class MojeDietetyczneFanaberieParser : IParser 
    {
        private static readonly string DivBodyIngredientsPattern = "//*[@class='post-body entry-content']";
        private static readonly string DivBodyDescriptionPattern = "//div[@class='post-body entry-content']";
        private static readonly string DivBodyImagePattern = "//*[@class='separator']";
        public Dictionary<int, string> ErrorRecipesList = new Dictionary<int, string>();
        public int RecipeId { get; set; }
        public HtmlDocument RecipeHtmlDocument { get; }
        private string RecipeToProcessUrl { get; }
        private HtmlWeb RecipeWebDocument { get; }
        public bool Process { get; set; } = true;

        public MojeDietetyczneFanaberieParser(string url)
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

        // //preceding-sibling::text()
        // //following-sibling::text()
        public List<string> GetDescription()
        {
            var descriptionList = new List<string>();
            bool process = true;

            //var descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyDescriptionPattern}//following-sibling::text()");
            var descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes("//*[@class='MsoNormal']//text()");

            //if (descriptionLiElements == null)
            //{
            //    descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes("//*[@class='MsoNormal']//text()");
            //    if (descriptionLiElements == null)
            //    {
            //        descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes("//*[@class='separator']//text()");
            //        if (descriptionLiElements == null)
            //        {
            //           descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes("//*[@class='separator']//text()");
            //            if (descriptionLiElements == null)
            //            {
            //                descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyDescriptionPattern}//text()[not(ancestor::i)]");
            //                if (descriptionLiElements == null)
            //                {
            //                    descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyDescriptionPattern}//text()[not(ancestor::i)]");
            //                    if (descriptionLiElements == null)
            //                    {
            //                        ErrorRecipesList.Add(RecipeId, RecipeToProcessUrl);
            //                        process = false;

            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            if (descriptionLiElements == null)
            {
                descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyDescriptionPattern}//text()[not(ancestor::li or ancestor::i or ancestor::u or ancestor::span)]");
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
                    descriptionList.Add(element.InnerText.Replace("&nbsp;", " "));
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
                //ingredientLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyIngredientsPattern}/text()[preceding-sibling::span[text() ='składniki:'] and following-sibling::span]");
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
                                                        .Replace("&nbsp;", " "));
                }
            }

            return ingredientList;
        }

        public void GetTitle() //do usuniecia - bedziemy wyciagac z bazy
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
    }
}