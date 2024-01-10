namespace Marketing.Models
{
    public interface ICategoriesRepository
    {

    

        public IEnumerable<Category> GetCategories();

        public IEnumerable<Product> GetProductsByCategoryId(int id);
    }
}
