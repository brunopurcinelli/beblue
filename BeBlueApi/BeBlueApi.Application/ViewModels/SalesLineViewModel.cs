using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeBlueApi.Application.ViewModels
{
    public class SalesLineViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The IdSales is Required")]
        [DisplayName("Id da Venda")]
        public Guid IdSales { get; set; }

        [Required(ErrorMessage = "The IdDisc is Required")]
        [DisplayName("Id do Disco")]
        public Guid IdDisc { get; set; }

        [Required(ErrorMessage = "The Quantity is Required")]
        [DisplayName("Quantidade")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Price Unit is Required")]
        [DisplayName("Valor Unitário")]
        public decimal PriceUnit { get; set; }

        [DisplayName("Valor Total")]
        public decimal SalesPrice { get; set; }
                
        [DisplayName("Quantidade")]
        public decimal Cashback { get; set; }
    }
}
