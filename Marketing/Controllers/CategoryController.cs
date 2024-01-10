using Marketing.DB;
using Marketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketing.Controllers
{

  
    public class CategoryController : Controller
    {

        private readonly ICategoriesRepository _categoriesRepository;
    
        public CategoryController(ICategoriesRepository categoriesRepository) {

            _categoriesRepository = categoriesRepository;
        
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("Category/GetProducts/{categoryId}")]
        public JsonResult GetProducts(int categoryId)
        {
               var products = _categoriesRepository.GetProductsByCategoryId(categoryId);

            
            return Json(products);
        }
    }
}
