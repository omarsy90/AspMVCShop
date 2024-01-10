namespace Marketing.Models
{
    public interface IShopingCardRepository
    {

        public bool AddProduct(int productId, string userId);

        public bool RemoveProduct(string customerId, int productI);

        public decimal CalculateTheTotal(string customerId);

        public bool RemoveAllItems(string customerId);
        public  IEnumerable< ShopingCart> GetProductsInShopinfCard(string userId);
    }
}
