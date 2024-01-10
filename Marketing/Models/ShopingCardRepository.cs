using Marketing.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Marketing.Models
{


    [Authorize]
    public class ShopingCardRepository : IShopingCardRepository
    {
        private readonly MarketingContext  _marketingContext;

        public ShopingCardRepository(MarketingContext marketingContext)
        {
           _marketingContext= marketingContext;
        }

        public bool AddProduct(int productId, string userId)
        {
            try
            {




                var savesProduct = _marketingContext.ShopingCarts.FirstOrDefault(shopItem => shopItem.ProductID == productId && shopItem.CustomerID == userId);

                if (savesProduct == null)
                {
                    ShopingCart shopingCart = new ShopingCart();
                    shopingCart.ProductID = productId;
                    shopingCart.CustomerID = userId;
                    shopingCart.Quantity = 1; 

                    _marketingContext.ShopingCarts.Add(shopingCart);
                    _marketingContext.SaveChanges();

                }
                else
                {
                    savesProduct.Quantity += 1;
                    _marketingContext.SaveChanges();

                }

                return true;




            }
            catch (Exception ex)
            {
                return false;

            }

           

           
            
        }

        public decimal CalculateTheTotal(string customerId)
        {
            var items = _marketingContext.ShopingCarts.Include(item => item.Product).Where(item => item.CustomerID == customerId);

            decimal sum = 0;
            foreach(var record in items)
            {
                sum += record.Quantity * record.Product.ProductPrise;
            }

            return sum;
           
        }

        public IEnumerable<ShopingCart> GetProductsInShopinfCard(string userId)
        {
            return _marketingContext.ShopingCarts.Where(item=>item.CustomerID == userId).Include(item=>item.Product).ToList();
            
        }

        public bool RemoveAllItems(string customerId)
        {
            var items = _marketingContext.ShopingCarts.Where(item => item.CustomerID == customerId);

            try
            {
                _marketingContext.ShopingCarts.RemoveRange(items);
                _marketingContext.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
          
        }

        public bool RemoveProduct(string customerId,int productId)
        {
            ShopingCart shopingItem = null;
            try
            {
                shopingItem = _marketingContext.ShopingCarts.First(item => item.CustomerID == customerId && item.ProductID == productId);

            }catch(Exception ex)
            {
                return false;
            }

            if (shopingItem == null)
            {
                return false;
            }

            shopingItem.Quantity -= 1; 

            if(shopingItem.Quantity <= 0) 
            {
             // _marketingContext.ShopingCarts.Attach(shopingItem);
              _marketingContext.ShopingCarts.Remove(shopingItem);

            }
            _marketingContext.SaveChanges();

            return true;
        }
    }
}
