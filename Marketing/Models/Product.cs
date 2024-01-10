using Microsoft.Build.Framework;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace Marketing.Models
{
    public class Product
    {

        public int ID { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public string ProductName { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
       
        [Range(0,999999999.00)]
        public decimal ProductPrise { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(300)]
        public string? ProductDescription { get; set; }


       
        public string? ImgUrl { get; set; }


        public Category? ProductCategory { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public int? ProductCategoryID { get; set; }
    }
}
