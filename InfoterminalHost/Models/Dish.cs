using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class Dish
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("preisStudent")]
        public decimal StudentPrice { get; set; }

        [JsonProperty("preisMitarbeiter")]
        public decimal EmployeePrice { get; set; }

        [JsonProperty("preisGast")]
        public decimal GuestPrice { get; set; }

        [JsonProperty("Inhaltsstoff")]
        public string Ingredient { get; set; }
    }
}
