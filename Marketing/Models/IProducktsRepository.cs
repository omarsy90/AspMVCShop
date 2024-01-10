using Marketing.DB;

namespace Marketing.Models
{
    public interface IProducktsRepository
    {
       


        public IEnumerable<Product> GetProducts();
        public Product GetProductById(int id);
        public bool CreateProduct(Product product);
        public bool UpdateProduct(int productId, Product newProduct);
        
        public bool DeleteProduct(int productId);
     
    }
}
