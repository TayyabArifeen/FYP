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
            [Required(ErrorMessage = "Username is required")]
            [Column(nameof(UserName), TypeName = "nvarchar(1000)")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Email is required")]
            [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
            [Column(nameof(UserEmail), TypeName = "nvarchar(1000)")]
            public string UserEmail { get; set; }
            [Required(ErrorMessage = "Password is required")]
            [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
            [Column(nameof(UserPassword), TypeName = "nvarchar(1000)")]
            public string UserPassword { get; set; }
            [Required]
            [Column(nameof(UserRole), TypeName = "int")]
            public int UserRole { get; set; }        
    }
}

