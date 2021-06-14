using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFY.Models;
namespace ProjectFY.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly JIECContext _context;
        public ProductDetailsController(JIECContext context)
        {
            this._context=context;
        }
        public IActionResult getAllProductDetails()
        {            
            var productDetails=this._context.ProductDetails.Include(c=>c.Product).ToList();
            return this.View(productDetails);
        }
    }
}