using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;


namespace InternetServiceBack.Dtos.User
{
    public class TokenSessionDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("dateExpired")]
        public DateTime DateExpired { get; set; }

        public static TokenSessionDto FromJson(string json)
        {
            return JsonConvert.DeserializeObject<TokenSessionDto>(json, Converter.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }
    }
}
