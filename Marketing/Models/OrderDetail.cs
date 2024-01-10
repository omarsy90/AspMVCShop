namespace Marketing.Models
{
    public class OrderDetail
    {

        public int Id { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }


        public int ProductId { get; set; }
        public Product Product {  get; set; }

        public int Qauntity { get; set; }

    }
}
