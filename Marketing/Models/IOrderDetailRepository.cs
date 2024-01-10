namespace Marketing.Models
{
    public interface IOrderDetailRepository
    {


        public IEnumerable<OrderDetail> GetOrderDetail(string orderId);

        public Task<bool> Add(OrderDetail orderDetail);
        public Task<bool> AddRange(IEnumerable<OrderDetail> orderDetailsList);

    }
}
