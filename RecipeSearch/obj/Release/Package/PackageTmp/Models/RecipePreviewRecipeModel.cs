using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RecipeSearch.Models
{
    public class RecipePreviewRecipeModel
    {
        public string Id { get; set; }
        public string Blog { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Image_Url { get; set; }
    }
}