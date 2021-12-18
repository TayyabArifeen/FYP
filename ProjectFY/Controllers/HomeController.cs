using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronXL;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ProjectFY.Models;

namespace ProjectFY.Controllers
{
    public class HomeController : Controller
    {
        private readonly JIECContext _context;
        public HomeController(JIECContext context)
        {
            this._context = context;
        }
        public IActionResult LoadFromWeb(string category, string url)
        {
            try
            {
                
                ReadFromWeb(category,url);
                return this.RedirectToAction("Scrapper");
            }
            catch(Exception ex)
            {
                return this.RedirectToAction("Scrapper");
            }
        }
        public void ReadFromWeb(string category, string url)
        {
            
            IWebDriver driver = new ChromeDriver(@"C:\Users\Arife\source\repos\ProjectFY\ProjectFY\bin\Debug");
            var Url = url;
            driver.Navigate().GoToUrl("https://www.daraz.pk/");
            //driver.Manage().Window.Maximize();
            var element = driver.FindElement(By.XPath("//*[@id=\"q\"]")); 
            var cat = category;
            element.SendKeys(cat);
            element.Submit();
            //var price = driver.FindElements(By.ClassName("c3gUW0"));
            var items = driver.FindElements(By.ClassName("c16H9d"));
            var total = driver.FindElement(By.ClassName("ant-pagination-item-102")).Text.ToString();
            Console.WriteLine(total);
            int t = Convert.ToInt32(total);
            //var sales = driver.FindElements(By.ClassName("c15YQ9"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement SearchResult;
            string rating = "";
            string price = "";
            string name = "";
            string sku = "";
            string rat = "";
            string month = "";
            string pri = "";
            string subcat = "";
            string[] months = { "January", "Feburary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int count = 0;
            int jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;

            for (int i = 1; i <= t; i++)
            {
                try
                {
                    try
                    {
                        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        //*[@id="root"]/div/div[3]/div[1]/div/div[1]/div[2]/div[1]/div/div/div[2]/div[2]
                        //System.Threading.Thread.Sleep(100);
                        SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div/div[1]/div[2]/div[" + i + "]/div/div/div[2]/div[2]")));
                        SearchResult.Click();
                    }
                    catch (Exception)
                    {
                        //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        //SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("c16H9d")));
                        //stem.Threading.Thread.Sleep(100);
                        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id=\"root\"]/div/div[3]/div[1]/div/div[1]/div[2]/div[" + i + "]/div/div/div[2]/div[2]")));
                        SearchResult.Click();
                    }
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("pdp-mod-product-badge-wrapper")));
                    name = driver.FindElement(By.ClassName("pdp-mod-product-badge-wrapper")).Text.ToString();
                    //System.Threading.Thread.Sleep(1000);

                    //sku = driver.FindElement(By.XPath("//*[@id=\"module_product_detail\"]/div/div[1]/div[3]/div/ul/li[2]/div")).Text.ToString();
                    try
                    {
                        sku = driver.FindElements(By.ClassName("key-value"))[1].Text.ToString();
                    }
                    catch (Exception)
                    {
                        sku = driver.FindElements(By.ClassName("key-value"))[0].Text.ToString();
                    }
                    //System.Threading.Thread.Sleep(1000);
                    price = driver.FindElement(By.ClassName("pdp-price")).Text.ToString();
                    //System.Threading.Thread.Sleep(1000);
                    rating = driver.FindElement(By.ClassName("count")).Text.ToString();
                    Console.WriteLine("Name: " + name);

                    int indx1;
                    pri = price.Substring(4);
                    string pstr = "";
                    if (pri.Contains(','))
                    {
                        //1,000,000
                        string p1 = (pri.Split(','))[0];
                        string p2 = (pri.Split(','))[1];
                        pstr = p1 + p2;
                        //1000,000
                        //if (pstr.Contains(','))
                        //{
                        //    string p3 = (pstr.Split(','))[0];
                        //    string p4 = (pstr.Split(','))[1];
                        //    pstr = p3 + p4;
                        //}
                        //else
                        //{

                        //}
                        Console.WriteLine("Price: " + pstr);
                    }
                    else
                    {
                        pstr = pri;
                        Console.WriteLine("Price: " + pstr);
                    }
                    indx1 = rating.IndexOf(' ');
                    rat = rating.Substring(0, indx1 + 1);
                    int a = Convert.ToInt32(rat);
                    Console.WriteLine("Rating: " + rat);
                    //System.Threading.Thread.Sleep(1000);

                    //if (sku == null || sku == "")
                    //{
                    //    sku = driver.FindElements(By.ClassName("key-value"))[0].Text.ToString();
                    //    Console.WriteLine(sku);
                    //}
                    //else
                    //{
                    //    Console.WriteLine(sku);
                    //}
                    if (a == 0)
                    {
                        Console.WriteLine(0);
                        jan = 0; feb = 0; mar = 0; apr = 0; may = 0; jun = 0; jul = 0; aug = 0; sep = 0; oct = 0; nov = 0; dec = 0;

                    }
                    else if (a > 0 && a <= 5)
                    {
                        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id=\"module_product_review\"]/div/div[3]/div[1]/div[" + count + "]/div[1]/span")));
                        //System.Threading.Thread.Sleep(1000);
                        //month = driver.FindElement(By.XPath("//*[@id=\"module_product_review\"]/div/div[3]/div[1]/div[" + count + "]/div[1]/span")).Text.ToString();
                        month = driver.FindElements(By.CssSelector(".title.right"))[count].Text.ToString();
                        //System.Threading.Thread.Sleep(1000);
                        string m = month.Substring(3, 3);

                        count = 0;
                        jan = 0; feb = 0; mar = 0; apr = 0; may = 0; jun = 0; jul = 0; aug = 0; sep = 0; oct = 0; nov = 0; dec = 0;
                        if (m.Equals("Jan"))
                        {
                            jan++;
                        }
                        else if (m.Equals("Feb"))
                        {
                            feb++;
                        }
                        else if (m.Equals("Mar"))
                        {
                            mar++;
                        }
                        else if (m.Equals("Apr"))
                        {
                            apr++;
                        }
                        else if (m.Equals("May"))
                        {
                            may++;
                        }
                        else if (m.Equals("Jun"))
                        {
                            jun++;
                        }
                        else if (m.Equals("Jul"))
                        {
                            jul++;
                        }
                        else if (m.Equals("Aug"))
                        {
                            aug++;
                        }
                        else if (m.Equals("Sep"))
                        {
                            sep++;
                        }
                        else if (m.Equals("Oct"))
                        {
                            oct++;
                        }
                        else if (m.Equals("Nov"))
                        {
                            nov++;
                        }
                        else if (m.Equals("Dec"))
                        {
                            dec++;
                        }
                        else { }
                    }
                    else
                    {

                        for (int j = 0; j < a; j++)
                        {

                            //SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id=\"module_product_review\"]/div/div[3]/div[1]/div[" + count + "]/div[1]/span")));
                            //System.Threading.Thread.Sleep(1000);
                            //month = driver.FindElement(By.XPath("//*[@id=\"module_product_review\"]/div/div[3]/div[1]/div[" + count + "]/div[1]/span")).Text.ToString();
                            try
                            {
                                try
                                {
                                    System.Threading.Thread.Sleep(500);
                                    month = driver.FindElements(By.CssSelector(".title.right"))[count].Text.ToString();
                                    count = 0;
                                }
                                catch (ArgumentOutOfRangeException e)
                                {
                                    month = "30 Sep 2020";
                                    if (cat == "Electronics")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Electronics&_keyori=ss&from=input&spm=a2a0e.home.search.go.35e34937P3IQch");
                                    }
                                    else if (cat == "Health")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Health&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.43bf5c62s4PKsU");
                                    }
                                    else if (cat == "Mobiles")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Mobiles&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.63fa29c0KvqNqP");
                                    }
                                    else if (cat == "Fashion")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Fashion&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.288129b2FRlbFB");
                                    }
                                    else if (cat == "Laptops")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Laptops&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.289623bbuITshe");
                                    }
                                    else if (cat == "Sports")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Sports&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.2339774fZOITNY");
                                    }
                                    else if (cat == "Automative")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?spm=a2a0e.searchlist.search.1.56be6e6d4hgqdJ&q=automative&_keyori=ss&clickTrackInfo=textId--6209398949348879423__abId--217068__pvid--ad253bce-e597-4d2a-b581-e11ee42ba284&from=suggest_normal&sugg=automative_0_1");
                                    }
                                    else if (cat == "Home Appliances")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?spm=a2a0e.searchlist.search.1.a0f85de2OeZ7oZ&q=home%20appliances&_keyori=ss&clickTrackInfo=textId--3565491136051202905__abId--217068__pvid--cfe5e60f-9e34-46be-b5a1-111b3771836e&from=suggest_normal&sugg=home%20appliances_0_1");
                                    }
                                    else if (cat == "Watches")
                                    {
                                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Watches&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.2abd6a6c6VDCwL");
                                    }
                                    Console.WriteLine(e);
                                }
                                //foreach(char k in month)
                                //{
                                //    Console.WriteLine(month);
                                //}
                                string m = month.Substring(3, 3);
                                if (j > 0 && j % 4 == 0 && (4 / j == 0))
                                {

                                    var button = driver.FindElement(By.ClassName("next-icon-last"));
                                    System.Threading.Thread.Sleep(500);
                                    button.Click();
                                    count = 0;
                                }

                                else
                                {
                                    count++;
                                }
                                if (m.Equals("Jan"))
                                {
                                    jan++;
                                }
                                else if (m.Equals("Feb"))
                                {
                                    feb++;
                                }
                                else if (m.Equals("Mar"))
                                {
                                    mar++;
                                }
                                else if (m.Equals("Apr"))
                                {
                                    apr++;
                                }
                                else if (m.Equals("May"))
                                {
                                    may++;
                                }
                                else if (m.Equals("Jun"))
                                {
                                    jun++;
                                }
                                else if (m.Equals("Jul"))
                                {
                                    jul++;
                                }
                                else if (m.Equals("Aug"))
                                {
                                    aug++;
                                }
                                else if (m.Equals("Sep"))
                                {
                                    sep++;
                                }
                                else if (m.Equals("Oct"))
                                {
                                    oct++;
                                }
                                else if (m.Equals("Nov"))
                                {
                                    nov++;
                                }
                                else if (m.Equals("Dec"))
                                {
                                    dec++;
                                }
                                else { }
                            }
                            catch (StaleElementReferenceException e)
                            {
                                if (cat == "Electronics")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Electronics&_keyori=ss&from=input&spm=a2a0e.home.search.go.35e34937P3IQch");
                                }
                                else if (cat == "Health")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Health&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.43bf5c62s4PKsU");
                                }
                                else if (cat == "Mobiles")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Mobiles&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.63fa29c0KvqNqP");
                                }
                                else if (cat == "Fashion")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Fashion&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.288129b2FRlbFB");
                                }
                                else if (cat == "Laptops")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Laptops&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.289623bbuITshe");
                                }
                                else if (cat == "Sports")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Sports&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.2339774fZOITNY");
                                }
                                else if (cat == "Automative")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?spm=a2a0e.searchlist.search.1.56be6e6d4hgqdJ&q=automative&_keyori=ss&clickTrackInfo=textId--6209398949348879423__abId--217068__pvid--ad253bce-e597-4d2a-b581-e11ee42ba284&from=suggest_normal&sugg=automative_0_1");
                                }
                                else if (cat == "Home Appliences")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?spm=a2a0e.searchlist.search.1.a0f85de2OeZ7oZ&q=home%20appliances&_keyori=ss&clickTrackInfo=textId--3565491136051202905__abId--217068__pvid--cfe5e60f-9e34-46be-b5a1-111b3771836e&from=suggest_normal&sugg=home%20appliances_0_1");
                                }
                                else if (cat == "Watches")
                                {
                                    driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Watches&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.2abd6a6c6VDCwL");
                                }
                                Console.WriteLine(e);
                            }

                        }

                    }
                    subcat = driver.FindElement(By.ClassName("breadcrumb")).Text.ToString();
                    int x = name.Length;
                    subcat.Substring(0, x);
                    Console.WriteLine("Category: " + subcat);
                    System.Threading.Thread.Sleep(4000);
                    int[] mo = { jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec };
                    jan = 0; feb = 0; mar = 0; apr = 0; may = 0; jun = 0; jul = 0; aug = 0; sep = 0; oct = 0; nov = 0; dec = 0;
                    try
                    {

                        using (StreamWriter w = new StreamWriter("DataSet.csv", true))
                        {
                            for (int k = 0; k < 12; k++)
                            {
                                var line = string.Format("{0},{1},{2},{3},{4},{5}", name, pstr, sku, mo[k], subcat, months[k]);
                                w.WriteLine(line);
                                w.Flush();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                    driver.Navigate().Back();

                }
                catch (Exception e)
                {
                    if (cat == "Electronics")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Electronics&_keyori=ss&from=input&spm=a2a0e.home.search.go.35e34937P3IQch");
                    }
                    else if (cat == "Health")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Health&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.43bf5c62s4PKsU");
                    }
                    else if (cat == "Mobiles")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Mobiles&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.63fa29c0KvqNqP");
                    }
                    else if (cat == "Fashion")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Fashion&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.288129b2FRlbFB");
                    }
                    else if (cat == "Laptops")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Laptops&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.289623bbuITshe");
                    }
                    else if (cat == "Sports")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Sports&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.2339774fZOITNY");
                    }
                    else if (cat == "Automative")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?spm=a2a0e.searchlist.search.1.56be6e6d4hgqdJ&q=automative&_keyori=ss&clickTrackInfo=textId--6209398949348879423__abId--217068__pvid--ad253bce-e597-4d2a-b581-e11ee42ba284&from=suggest_normal&sugg=automative_0_1");
                    }
                    else if (cat == "Home Appliences")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?spm=a2a0e.searchlist.search.1.a0f85de2OeZ7oZ&q=home%20appliances&_keyori=ss&clickTrackInfo=textId--3565491136051202905__abId--217068__pvid--cfe5e60f-9e34-46be-b5a1-111b3771836e&from=suggest_normal&sugg=home%20appliances_0_1");
                    }
                    else if (cat == "Watches")
                    {
                        driver.Navigate().GoToUrl("https://www.daraz.pk/catalog/?q=Watches&_keyori=ss&from=input&spm=a2a0e.searchlist.search.go.2abd6a6c6VDCwL");
                    }
                    Console.WriteLine(e);


                }
                try
                {
                    if (i % 40 == 0 && i > 0)
                    {

                        driver.FindElement(By.ClassName("ant-pagination-next")).Click();
                        System.Threading.Thread.Sleep(500);
                        items = driver.FindElements(By.ClassName("c16H9d"));
                    }
                    else
                    {
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public IActionResult HomePage()
        {
            return this.View();
        }
        public IActionResult Index()
        {
            try
            {
                this.readDataFromExcel();
                return this.RedirectToAction("LoadDataFromExcel");
            }
            catch (Exception e)
            {
                return this.RedirectToAction("LoadDataFromExcel");
            }
        }
        public float getPrice(string str)
        {
            var price = str.Remove(0, 4);
            var s = price.Split(',');
            var t = "";
            foreach (var r in s)
                t += r;
            float _price = (float)Convert.ToDouble(t);
            return _price;
        }
        public void readDataFromExcel()
        {

            WorkBook workbook = WorkBook.Load(@"DATASET.xlsx");
            WorkSheet sheet = workbook.WorkSheets.First();
            int _row = 0;
            foreach (var row in sheet.Rows)
            {
               
                Product product = new Product();
                ProductDetails productDetails = new ProductDetails();
                product.ProductName = row.Columns.ElementAt(0).ToString();
                product.ProductPrice = this.getPrice(row.Columns.ElementAt(1).ToString());
                product.SKUNumber = row.Columns.ElementAt(2).ToString();
                product.ProductCategory = "Electronics";
                product.ProductSubCategory = row.Columns.ElementAt(4).ToString();
                try
                {
                    if (product.SKUNumber != null && !this._context.Products.Any(c => c.SKUNumber == product.SKUNumber))
                    {
                        if (product.ProductName != null || product.SKUNumber != null || product.ProductSubCategory != null || product.ProductCategory != null)
                        {
                            this._context.Products.Add(product);
                            this._context.SaveChanges();
                        }
                    }
                    var pro = this._context.Products.FirstOrDefault(c => c.SKUNumber == product.SKUNumber);
                    if (this._context.ProductDetails.Where(c => c.ProductID == pro.ProductID).Count() <= 11)
                    {
                        productDetails.NumberOfSales = (float)Convert.ToDouble(row.Columns.ElementAt(3).ToString());
                        productDetails.MonthOfSale = row.Columns.ElementAt(5).ToString();
                        productDetails.ProductID = pro.ProductID;
                        if (productDetails.MonthOfSale != null)
                        {
                            this._context.ProductDetails.Add(productDetails);
                            this._context.SaveChanges();
                        }
                    }
                    _row += 1;
                }
                catch(Exception ex)
                {
                    _row += 1;
                }
                
            }
        }
        public IActionResult LoadDataFromExcel()
        {
            return this.View();
        }
        public IActionResult Scrapper()
        {
            return this.View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}