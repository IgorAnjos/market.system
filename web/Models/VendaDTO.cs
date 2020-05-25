using Newtonsoft.Json;

namespace mktSystem.Models
{
    public class VendaDTO
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public float Total { get; set; }

        [JsonProperty("troco", NullValueHandling = NullValueHandling.Ignore)]
        public float Troco { get; set; }

        [JsonProperty("produtos", NullValueHandling = NullValueHandling.Ignore)]
        public SaidaDTO[] Produtos { get; set; }
    }
}