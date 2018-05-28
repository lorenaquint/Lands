

namespace Lands.Models
{
    using Newtonsoft.Json;
    public class Language
    {


        [JsonProperty(PropertyName = "iso639_1")]
        public string Iso6391 { get; set; }
        [JsonProperty(PropertyName = "iso639_2")]
        public string Iso6392 { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "NativeName")]
        public string NativeName { get; set; }
    }

}
