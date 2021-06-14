using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectFY.Models;
namespace ProjectFY.Controllers
{
    public class ProductsController : Controller
    {
        private readonly JIECContext _context;
        public ProductsController(JIECContext context)
        {
            this._context=context;
        }
        public IActionResult productList()
        {
            var products=this._context.Products.ToList();
            return this.View(products);
        }
    }
}