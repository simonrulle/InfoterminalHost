using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class ExtendedDish : Dish
    {
        public string Date { get; set; }
        public string CategoryName { get; set; }
    }
}
