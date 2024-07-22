using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public partial class Intent
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("confidenceScore")]
        public decimal ConfidenceScore { get; set; }
    }
}
