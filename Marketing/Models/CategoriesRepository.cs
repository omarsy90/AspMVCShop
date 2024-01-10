using Marketing.DB;

namespace Marketing.Models
{
    public class CategoriesRepository : ICategoriesRepository
    {
       
        private MarketingContext _marketingContext { set; get; }
        public CategoriesRepository( MarketingContext marketingContext) {

            _marketingContext = marketingContext;

            

        }


        private void SaveChange()
        {
            _marketingContext.SaveChanges();
        }



        public IEnumerable<Category> GetCategories()
        {
            return _marketingContext.Categories;
          
        }


        public IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            if(id <= 0)
            {
                return _marketingContext.Products;
            }

            return _marketingContext.Products.Where(product =>product.ProductCategoryID == id);
        }


    
    }
}
