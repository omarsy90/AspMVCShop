using Marketing.Areas.Identity.Data;
using Marketing.Models;


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using Stripe.Checkout;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace Marketing.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProducktsRepository _Productsrepository;
        private readonly ICategoriesRepository _CategoriesRepository;
      
        public HomeController(IProducktsRepository produckts, ICategoriesRepository categories, UserManager<MarketingUser> userManager)
        {
           _Productsrepository= produckts;
            _CategoriesRepository= categories;
   
        }

        public IActionResult Index()
        {

            IEnumerable<Product> products = _Productsrepository.GetProducts();
            IEnumerable<Category> categories = _CategoriesRepository.GetCategories();
            ViewBag.Categories = categories;
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [HttpGet("home/{id}")]
         public IActionResult Detail(int id )
        { 
            var product = _Productsrepository.GetProductById(id);

            return View(product);
        }


        [HttpGet("home/error/{msg?}")]
        public IActionResult Error(string msg)
        {
            var t = Request.QueryString;
            ViewBag.Error = msg;
            return View();
        }


       


       

    
        
        
    
    }
}