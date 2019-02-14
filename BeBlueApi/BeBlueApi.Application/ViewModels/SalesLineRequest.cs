using Newtonsoft.Json;
using System;

namespace BeBlueApi.Application.ViewModels
{
    public class SalesLineRequest
    {
        [JsonProperty("Id do Disco")]
        public Guid IdDisc { get; set; }
    
        [JsonProperty("Quantidade")]
        public int Quantity { get; set; }

        [JsonProperty("Valor Unitário")]
        public decimal PriceUnit { get; set; }
    }
}
