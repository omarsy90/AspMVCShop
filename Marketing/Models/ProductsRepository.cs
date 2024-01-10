using Marketing.DB;
using Microsoft.EntityFrameworkCore;

namespace Marketing.Models
{
    public class ProductsRepository : IProducktsRepository
    {
       

        private MarketingContext _MarketingContext { set; get; }




        public ProductsRepository(MarketingContext marketingContext)
        {

            _MarketingContext= marketingContext;
         
           
        }

        public IEnumerable<Product> GetProducts()
        {
            return _MarketingContext.Products;
        }


        public bool CreateProduct(Product product)
        {
            try
            {

                _MarketingContext.Products.Add(product);
                SaveChange();
            }
            catch(Exception ex)
            {
                return false;
            }

           
            return true;
                
        }


        public Product GetProductById(int id)
        {

            return _MarketingContext.Products.Where(pro => pro.ID == id).Include(pro => pro.ProductCategory).FirstOrDefault();
        }
        public bool UpdateProduct(int productId, Product newProduct)
        {
            try
            {
                
                 var  product = _MarketingContext.Products.FirstOrDefault(product => product.ID == productId);

                product.ProductName = newProduct.ProductName;
                product.ProductPrise = newProduct.ProductPrise;
                product.ProductDescription = newProduct.ProductDescription;
                product.ProductCategoryID = newProduct.ProductCategoryID;


                SaveChange();
                return true;

            }catch(Exception ex)
            {

                return false;
            }

            
        }


        public bool DeleteProduct(int productId)
        {
            var product = GetProductById(productId);
            if(product!= null)
            {
                try
                {
                    _MarketingContext.Products.Attach(product);
                    _MarketingContext.Products.Remove(product);
                    SaveChange();
                }catch(Exception ex)
                {
                    return false;
                }
                
                return true;
            }
            return false;

        }

        private void SaveChange()
        {
            _MarketingContext.SaveChanges();
        }

       

        
    }
}
