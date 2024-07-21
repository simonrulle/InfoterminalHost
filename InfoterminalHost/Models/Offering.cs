using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Models
{
    public class Offering
    {
        [JsonProperty("datum")]
        public string Date { get; set; }

        [JsonProperty("hinweisDaten")]
        public NoteData NoteData { get; set; }

        [JsonProperty("rubrik")]
        public Category[] Categories { get; set; }
    }
}
