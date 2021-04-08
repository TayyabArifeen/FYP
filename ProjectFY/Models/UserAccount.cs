using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFY.Models
{
    public class UserAccount
    {
        [Key]
        [Required]
        [Column(nameof(UserId), TypeName = "int")]
        public int UserId { get; set; }
        [Required]
        [Column(nameof(Name), TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Required]
        [Remote(action:"IsEmailInUse",controller:"Login")]
        [Column(nameof(Email), TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Required]
        [Column(nameof(Password), TypeName = "nvarchar(100)")]
        public string Password { get; set; }

    }
}
