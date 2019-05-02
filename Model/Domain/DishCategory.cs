using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class DishCategory
    {
        public int DishCategoryId { get; set; }
        public string DishCategoryName { get; set; }
        public int DishCategoryParentId { get; set; }
    }
}
