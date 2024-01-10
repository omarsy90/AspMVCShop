namespace Marketing.Models
{
    public interface IOrderRepository
    {
        public Task<bool> AddOrder(Order order);

        public Task<List<Order>> GetOrders();

        public Task<Order> GetOrderById(string id);
        public Task<bool> EditStatus(string orderid, StatusDeleviring status);

        public Task<bool> DelelteOrder(string orderid);

    }
}
