namespace Marketing.Models
{
    public class ViewModelOrder
    {

        public string OrderID { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime Timestamp { get; set; }

        public StatusDeleviring StatusDeleviring { get; set; }

        public decimal Total { get; set; }
        public IEnumerable<ViewModelOrderDetail> DetailsInfo { get; set; }

    }
}
