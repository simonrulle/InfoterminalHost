using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class JsonResult
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("result")]
        public CLUOutput Result { get; set; }
    }
}
