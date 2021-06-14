using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFY.Models
{
    public class JIECContext : DbContext
    {
        public JIECContext(DbContextOptions options):base(options)
        {

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetails> ProductDetails { get; set; }
        public virtual DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasAlternateKey(a => new { a.UserEmail });
            builder.Entity<Product>().HasAlternateKey(a => new {a.SKUNumber  });
            builder.Entity<ProductDetails>().HasOne(a => a.Product).WithMany(b => b.ProductDetails).HasForeignKey(f => f.ProductID).HasPrincipalKey(p => p.ProductID);
        }
    }
    
}
