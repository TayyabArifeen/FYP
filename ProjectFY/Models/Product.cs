using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFY.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Required]
        [Column(nameof(ProductID), TypeName = "int")]
        public int ProductID { get; set; }

        [Required]
        [Column(nameof(SKUNumber), TypeName = "nvarchar(1000)")]
        public string SKUNumber { get; set; }
        [Required]
        [Column(nameof(ProductName), TypeName = "nvarchar(1000)")]
        public string ProductName { get; set; }
        [Required]
        [Column(nameof(ProductCategory), TypeName = "nvarchar(1000)")]
        public string ProductCategory { get; set; }
        [Required]
        [Column(nameof(ProductSubCategory), TypeName = "nvarchar(1000)")]
        public string ProductSubCategory { get; set; }
        [Required]
        [Column(nameof(ProductPrice), TypeName = "float")]
        public float ProductPrice { get; set; }
        [NotMapped]
        public float PredictionValue { get;set;}
        public List<ProductDetails> ProductDetails { get; set; }
    }
}
