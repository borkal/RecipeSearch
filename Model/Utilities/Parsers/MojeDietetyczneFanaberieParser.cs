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
        //FANABERIE PRZEPISY ID 2229, 2218 - DO SPRAWDZENIA CO TO JEST! kilka przepisow w jednym linku??
        //przepisy dwujezyczne - np. 1367 - co z tym??
        //1396, 1414 ???
        //tytuly nie zawsze zawieraja nazwe przepisu - jak to bedzie z wyszukiwarka?
        //1426 - zadanie zostalo przerwane - problem z polaczeniem z baza? do sprawdzenia, bo skladniki wypisalo
        private static readonly string DivBodyIngredientsPattern = "//*[@class='post-body entry-content']";
        private static readonly string DivBodyDescriptionPattern = "//*[@class='post-body entry-content']";
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
            try //potrzebne??
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

            var descriptionLiElements = RecipeHtmlDocument.DocumentNode.SelectNodes($"{DivBodyDescriptionPattern}//following-sibling::text()");

            if (descriptionLiElements == null)
                {
                    ErrorRecipesList.Add(RecipeId, RecipeToProcessUrl);
                    process = false;
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

//SELECT wyszukujący ilość przepisów z danego bloga
//Select rs.name, count(r.*) AS NumberOfRecipes from recipe r
//left join recipesource rs on r.source_id = rs.id
//GROUP BY rs.name
//ORDER BY NumberOfRecipes DESC
