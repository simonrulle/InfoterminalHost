using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class MealPlan
    {
        [JsonProperty("angebot")]
        public Offering[] Offering { get; set; }
    }
}
