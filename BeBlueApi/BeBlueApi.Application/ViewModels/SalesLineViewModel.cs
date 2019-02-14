using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeBlueApi.Application.ViewModels
{
    public class SalesLineViewModel
    {
        [Key]
        [JsonProperty("Id da Linha de Venda")]
        public Guid Id { get; set; }
        
        [JsonProperty("Id da Venda")]
        public Guid IdSales { get; set; }

        [JsonProperty("Id do Disco")]
        public Guid IdItem { get; set; }
        
        [JsonProperty("Nome do Disco")]
        public string DiscName { get; set; }

        [Required(ErrorMessage = "A quantidade de itens é obrigatória")]
        [JsonProperty("Quantidade")]
        public int Quantity { get; set; }

        [JsonProperty("Valor Unitário")]
        public decimal PriceUnit { get; set; }

        [JsonProperty("Valor Total")]
        public decimal SalesPrice { get; set; }

        [JsonProperty("Cashback")]
        public decimal Cashback { get; set; }
    }
}
