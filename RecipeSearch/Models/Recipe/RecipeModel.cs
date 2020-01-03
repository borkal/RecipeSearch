using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Domain;
using Model.Domain.Recipe;
using RecipeSearch.Models.Recipe;


namespace RecipeSearch.Models
{
    public class RecipeModel
    {
        public string Blog { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Description { get; set; }
        public string Source_Url { get; set; }
        public string Id { get; set; }
        public string Image_Url { get; set; }
        public string Blog_Url { get; set; }
        public string Title { get; set; }
        public RecipeTotalRate Rate { get; set; }
        
    }
}