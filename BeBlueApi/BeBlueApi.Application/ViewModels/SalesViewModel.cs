using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeBlueApi.Application.ViewModels
{
    public class SalesViewModel
    {
        [Key]
        [JsonProperty("Id da Venda")]
        public Guid Id { get; set; }
        
        [JsonProperty("Data da Venda")]
        public DateTime SalesDate { get; set; }
        
        [JsonProperty("Valor total da Venda")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("Total do Cashback")]
        public decimal TotalCashback { get; set; }

        [JsonProperty("Linhas de Venda")]
        public virtual ICollection<SalesLineViewModel> SalesLines { get; set; }
    }
}
