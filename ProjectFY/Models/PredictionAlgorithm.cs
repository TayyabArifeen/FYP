using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFY.Models
{
    public class PredictionAlgorithm
    {
        private readonly JIECContext _context;
        public List<Product> products=new List<Product>();
        List<double> xValues = new List<double>();
        List<double> yValues = new List<double>();
        public int NumberOfProducts;
        public string category="";
        public float investment;
        public PredictionAlgorithm(JIECContext context,string category,float investment,int numberOfProducts)
        {
            this.NumberOfProducts=numberOfProducts;
            this._context=context;
            if(category!=null)
                this.category=category.ToLower();
            this.investment=investment;            
        }
        public List<Product> getAllProductsPredictions()
        {
            this.products = this._context.Products.Where(c => c.ProductCategory.ToLower().Contains(this.category)).ToList();
            foreach (var product in this.products)
            {
                foreach (var productDetails in this._context.ProductDetails.Include(c => c.Product).Where(c => c.ProductID == product.ProductID).ToList())
                {
                    this.xValues.Add(this.getMonth(productDetails.MonthOfSale));
                    this.yValues.Add(productDetails.NumberOfSales);
                }
                product.PredictionValue = (float)this.Prediction();
            }            
            var investments=this.getInvestment();
            this.products= this.products.Where(c=>c.ProductPrice<=investments).OrderByDescending(c=>c.PredictionValue).ToList();
            return this.products;
        }
        public float getInvestment()
        {            
            float result=0;
            for(int i=10000;i<=this.investment;i+=10000)
            {                
                result=i/this.NumberOfProducts;
            }
            return result;
        }
        public int getMonth(string str)
        {
            if (str.Count() > 0 || str != null)
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
        public double Prediction()
        {                        
            double rSquared, intercept, slope;
            this.LinearRegression(out rSquared, out intercept, out slope);            
            var predictedValue = (slope * 13) + intercept;
            return predictedValue;
        }
      
        public void LinearRegression(out double rSquared,out double yIntercept,out double slope)
        {
            if (this.xValues.Count!=this.yValues.Count)
            {
                throw new Exception("Input values should be with the same length.");
            }

            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;

            for (var i = 0; i < this.xValues.Count; i++)
            {
                var x = this.xValues.ElementAt(i);
                var y = this.yValues.ElementAt(i);
                //SumXY
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            //n
            var count = this.xValues.Count;
            var ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            var ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            var rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            var rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            var sCo = sumCodeviates - ((sumOfX * sumOfY) / count);
            var meanX = sumOfX / count;
            var meanY = sumOfY / count;
            var dblR = rNumerator / Math.Sqrt(rDenom);
            rSquared = dblR * dblR;
            yIntercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }
    }
}
