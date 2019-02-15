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
        public Guid Id { get; set; }
        
        [DisplayName("Data da Venda")]
        public DateTime SalesDate { get; set; }
        
        [DisplayName("Total da Venda")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Total do Cashback")]
        public decimal TotalCashback { get; set; }
    }
}
