using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class IngredientCategoryXref
    {
        public int IngredientId { get; set; }
        public int IngredientCategoryIds { get; set; }
    }
}
