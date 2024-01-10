namespace Marketing.Models
{
    public class ShopingCart
    {

        public int ID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
       public Customer Customer { get; set; }

        public string CustomerID { get; set; }

    }
}
