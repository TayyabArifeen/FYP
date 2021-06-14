using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFY.Models
{
    [Table("User")]
    public class User
    {        
            [Key]
            [Required]
            [Column(nameof(UserID), TypeName = "int")]
            public int UserID { get; set; }                                    
            [Required]
            [Column(nameof(UserName), TypeName = "nvarchar(1000)")]
            public string UserName { get; set; }
            [Required]
            [Column(nameof(UserEmail), TypeName = "nvarchar(1000)")]
            public string UserEmail { get; set; }
            [Required]
            [Column(nameof(UserPassword), TypeName = "nvarchar(1000)")]
            public string UserPassword { get; set; }
            [Required]
            [Column(nameof(UserRole), TypeName = "int")]
            public int UserRole { get; set; }        
    }
}

