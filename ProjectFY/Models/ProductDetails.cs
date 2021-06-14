using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFY.Models
{
    [Table("ProductDetails")]
    public class ProductDetails
    {
        [Key]
        [Required]
        [Column(nameof(ProductDetailsID), TypeName = "int")]
        public int ProductDetailsID { get; set; }
        [Required]
        [Column(nameof(ProductID), TypeName = "int")]
        public int ProductID { get; set; }
        [Required]
        [Column(nameof(NumberOfSales), TypeName = "float")]
        public float NumberOfSales { get; set; }
        [Required]
        [Column(nameof(MonthOfSale), TypeName = "nvarchar(1000)")]
        public string MonthOfSale { get; set; }
        [NotMapped]
        public int Month { get;set;}
        public Product Product { get;set;}
    }
}
