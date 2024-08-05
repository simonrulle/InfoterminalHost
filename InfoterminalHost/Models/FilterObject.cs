using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public partial class FilterObject
    {
        public string Building { get; set; }
        public string CategoryName { get; set; }
        public DateInfo DateInfo { get; set; }
        public string DishName { get; set; }
        public string Email { get; set; }
        public string Faculty { get; set; }
        public string Ingredient { get; set; }
        public string PersonName { get; set; }
        public string PhoneNumber { get; set; }
        public string Price { get; set; }
        public string PriceCategory { get; set; }
        public string PriceInterpretation { get; set; }
        public string Role { get; set; }
        public string Room { get; set; }
        public string Title { get; set; }
    }
}
