using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public partial class Prediction
    {
        [JsonProperty("topIntent")]
        public string? TopIntent { get; set; }

        [JsonProperty("projectKind")]
        public string? ProjectKind { get; set; }

        [JsonProperty("intents")]
        public List<Intent>? Intents { get; set; }

        [JsonProperty("entities")]
        public List<Entity>? Entities { get; set; }
    }
}



