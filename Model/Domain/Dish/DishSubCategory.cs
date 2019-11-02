using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class DishSubCategory
    {
        public int DishSubCategoryId { get; set; }
        public string DishSubCategoryName { get; set; }
        public int DishMainCategoryId { get; set; }
    }
}
