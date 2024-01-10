namespace Marketing.Models
{

    public enum StatusDeleviring
    {
        created = 0,
        processing =1,
        delivering =2,
        arrived=3,

    }
    public class Order
    {
      public string OrderId { get; set; }
    

        public string CustomerId { set; get; } = string.Empty;
        public Customer Customer { get; set; }

      public StatusDeleviring statusDeleviring { get; set; } = StatusDeleviring.created;
      
       public  DateTime Timestamp { set; get; }


        public decimal Total { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }


    }
}
