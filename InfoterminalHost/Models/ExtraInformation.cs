using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public partial class ExtraInformation
    {
        [JsonProperty("extraInformationKind")]
        public string ExtraInformationKind { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
