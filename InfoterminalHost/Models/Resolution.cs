using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public partial class Resolution
    {
        [JsonProperty("resolutionKind")]
        public string ResolutionKind { get; set; }

        [JsonProperty("dateTimeSubKind")]
        public string DateTimeSubKind { get; set; }

        [JsonProperty("timex")]
        public string Timex { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("begin")]
        public string Begin { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }
    }
}
