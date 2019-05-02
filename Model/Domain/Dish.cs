using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Dish
    {
        public int DishId { get; set; }
        public string DishDescription { get; set; }
        public string DishName { get; set; }
        public DishCategory DishCategory { get; set; }
    }
}
