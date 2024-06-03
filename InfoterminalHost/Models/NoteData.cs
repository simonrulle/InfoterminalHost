using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class NoteData
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("textOben")]
        public string TextAbove { get; set; }

        [JsonProperty("textUnten")]
        public string TextBottom { get; set; }
    }
}
