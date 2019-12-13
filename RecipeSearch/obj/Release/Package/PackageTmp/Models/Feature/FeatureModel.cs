using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeSearch.Models.Feature
{
    public class FeatureModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}