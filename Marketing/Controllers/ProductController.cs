using Marketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Marketing.Controllers
{

    [Authorize(Roles ="admin")]
    public class ProductController : Controller
    {

        private readonly IProducktsRepository _producktsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ISavingFileHandler _savingFileHandler = new FileHandler();
        public ProductController(IProducktsRepository producktsRepository, ICategoriesRepository categoriesRepository)
        {
            _producktsRepository= producktsRepository;
            _categoriesRepository = categoriesRepository;
        }
       
        public IActionResult Index()
        {

            var products = _producktsRepository.GetProducts();
             return View("Index",products);
        }



       


        [HttpGet]

        public IActionResult Add()
        {
             var CategroriesList = _categoriesRepository.GetCategories();
            ViewBag.Categories = CategroriesList;
            return View("Create");
         
        }


        [HttpPost]
        public IActionResult Add(Product product)
        {

            var CategroriesList = _categoriesRepository.GetCategories();
            ViewBag.Categories = CategroriesList;


            if (ModelState.IsValid  && Request.Form.Files.Count >0 )
            {
                string imgUrlDb = string.Empty;
                bool isSaved = _savingFileHandler.Save(Request.Form.Files[0], out imgUrlDb);

                if (isSaved)
                {
                    // foto saved in server
                    product.ImgUrl= imgUrlDb;

                    _producktsRepository.CreateProduct(product);
                    ViewBag.SuccessMessage = "the product has been created";

                }
               
                return View("Create");
            }
            else
            {
                ViewBag.ErrorMessage = "error while saving prodcut";
                return View("Create",product);

            }

            
        }


        [HttpGet]
        [Route("Product/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            Product product = _producktsRepository.GetProductById(id);

            ViewBag.Categories = _categoriesRepository.GetCategories();

            return View("Edit",product);

        }

        [HttpPost]
        public IActionResult Edit(Product newproduct)
        {
          

           if(ModelState.IsValid)
            {
                _producktsRepository.UpdateProduct(newproduct.ID,newproduct);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = _categoriesRepository.GetCategories();
                ViewBag.ErrorMessage = "errors in inputed date";
                return View("Edit", newproduct);
            }

            
        }


        public IActionResult Details(int id)
        {
            var product = _producktsRepository.GetProductById(id);
            ViewBag.CategoryName = _categoriesRepository.GetCategories().FirstOrDefault(category=>category.ID == product.ProductCategoryID)?.CategoryName;
            return View(product);

        }


        [HttpGet]
        [Route("Product/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _producktsRepository.DeleteProduct(id);
            if(isDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return  BadRequest();
            }
        }



    }
}
