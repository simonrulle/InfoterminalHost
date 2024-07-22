using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public partial class Entity
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("confidenceScore")]
        public decimal ConfidenceScore { get; set; }

        [JsonProperty("resolutions")]
        public List<Resolution> Resolutions { get; set; }

        [JsonProperty("extraInformations")]
        public List<ExtraInformation> ExtraInformations { get; set; }
    }
}
