using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public partial class CLUOutput
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("prediction")]
        public Prediction Prediction { get; set; }
    }
}
