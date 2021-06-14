using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectFY.Models;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Microsoft.EntityFrameworkCore;

namespace ProjectFY.Controllers
{
    public class Prediction
    {
        public string Categories { get;set;}
        public float Investment { get;set;}
        public int count { get;set;}
        public float predictionValue { get;set;}
        public string skuNumberOfProduct { get; set; }
        public string nameOfProduct { get; set; }
        public int numberOfProducts { get;set;}
        public Prediction()
        {

        }
    }
    public class  Analysis
    {
        public List<Product> products=new List<Product>();
        public float Risk { get;set;}
        public int numberOfCompetitors { get;set;}
        public string SkuNumberOfProduct { get; set; }
        public string NameOfProduct { get; set; }
        public int totalSalesOfProduct { get; set; }
        public Analysis()
        {

        }
    }
    public class UserController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly JIECContext _context;
        public static Product _Product=new Product();
        public static List<Product> products=new List<Product>();
        public static Analysis Analysis=new Analysis();
        public UserController(JIECContext context,IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment=hostingEnvironment;
            this._context=context;
        }
        [HttpGet]
        public IActionResult Predictions()
        {                     
            return this.View(new Prediction());
        }
        public int getMonth(string str)
        {
            if(str.Count()>0 || str!=null)
            {
                if (str.ToLower() == "january")
                    return 1;
                else if (str.ToLower() == "february")
                    return 2;
                else if (str.ToLower() == "march")
                    return 3;
                else if (str.ToLower() == "april")
                    return 4;
                else if (str.ToLower() == "may")
                    return 5;
                else if (str.ToLower() == "june")
                    return 6;
                else if (str.ToLower() == "july")
                    return 7;
                else if (str.ToLower() == "august")
                    return 8;
                else if (str.ToLower() == "september")
                    return 9;
                else if (str.ToLower() == "october")
                    return 10;
                else if (str.ToLower() == "november")
                    return 11;
                else
                    return 12;
            }            
            else 
                return 0;
        }
        public IActionResult getAnalysis(string PdfView)
        {
            if(Analysis.products.Count==0 || products.Count==0)
                Analysis.Risk=0;
            else
            {
                Analysis.Risk = (Analysis.products.Count / products.Count) * 100;                
            }            
            this.ViewBag.pdf=PdfView;
            List<ProductDetails> ProductDetail = new List<ProductDetails>();
            this.ViewBag.Analysis = Analysis;
            
            foreach (var item in this._context.ProductDetails.Include(c => c.Product).Where(c => c.ProductID == _Product.ProductID).ToList())
            {
                item.Month=this.getMonth(item.MonthOfSale);
                ProductDetail.Add(item);
            }
            if (PdfView=="1")
            {
                if(Analysis.products.Count>7)
                {
                    Analysis.products.RemoveRange(7,products.Count-7);
                }

            }
                
            this.ViewBag.Data=ProductDetail;
            Analysis.numberOfCompetitors = Analysis.products.Count;
            return this.View(Analysis.products);
        }
        public IActionResult PredictionResult(string category,float investment,int Products)
        {            
            Prediction prediction = new Prediction();
            prediction.Categories = category;
            prediction.Investment = investment;
            prediction.numberOfProducts = Products;
            PredictionAlgorithm predictionAlgorithm = new PredictionAlgorithm(this._context, prediction.Categories, prediction.Investment, prediction.numberOfProducts);
            products = predictionAlgorithm.getAllProductsPredictions();
            if (products.Count == 0 || products == null)
                return this.View(products);
            prediction.predictionValue = products.ElementAt(0).PredictionValue;
            prediction.skuNumberOfProduct = products.ElementAt(0).SKUNumber;
            prediction.nameOfProduct = products.ElementAt(0).ProductName;
            _Product = products.ElementAt(0);
            this.ViewBag.HightestPrediction = products.ElementAt(0).PredictionValue;
            this.ViewBag.NameOfProduct = products.ElementAt(0).ProductName;
            this.ViewBag.SKUOfProduct = products.ElementAt(0).SKUNumber;
            Analysis.products = products.Where(c => c.ProductSubCategory == products.ElementAt(0).ProductSubCategory).ToList();            
            return this.View(products);
        }
        public IActionResult downloadAnalysis()
        {
        
            var url = "https://localhost:44374/User/getAnalysis?pdfview=1";            
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.Blink);

            BlinkConverterSettings settings = new BlinkConverterSettings();
            settings.AdditionalDelay=10000;
            settings.BlinkPath = Path.Combine(this._hostingEnvironment.ContentRootPath, "BlinkBinariesWindows");

            htmlConverter.ConverterSettings = settings;

            PdfDocument document = htmlConverter.Convert(url);

            MemoryStream stream = new MemoryStream();

            document.Save(stream);
            return this.File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Report.pdf");
        }
    }
}