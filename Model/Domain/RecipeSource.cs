using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class RecipeSource
    {
        public int RecipeSourceId { get; set; }
        public string RecipeSourceCanonicalUrl { get; set; }
        public string RecipeSourceName { get; set; }
        public string RecipeSourceRegexp { get; set; }
    }
}
