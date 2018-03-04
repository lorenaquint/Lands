

namespace Lands.Models
{
    using Newtonsoft.Json;
    public class Language
    {


        [JsonProperty(PropertyName = "iso6391")]
        public string Iso639_1 { get; set; }
        [JsonProperty(PropertyName = "iso6392")]
        public string Iso639_2 { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "NativeName")]
        public string NativeName { get; set; }
    }

}
