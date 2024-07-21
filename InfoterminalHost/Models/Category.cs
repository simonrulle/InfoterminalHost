using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("gericht")]
        public Dish[] Dishes { get; set; }
    }
}
