using Marketing.DB;

namespace Marketing.Models
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private readonly MarketingContext _marketingContext;


        public OrderDetailRepository(MarketingContext marketingContext)
        {
            _marketingContext = marketingContext;
        }

        public Task<bool> Add(OrderDetail orderDetail)
        {

         return   Task.Run(async () =>
            {
                try
                {
                    await _marketingContext.OrdersDetails.AddAsync(orderDetail);
                  await  _marketingContext.SaveChangesAsync();
                    return true;

                }catch(Exception e)
                {
                    return false;

                }
                


            });
            
        }

        public Task<bool> AddRange(IEnumerable<OrderDetail> orderDetailsList)
        {
           return Task.Run(async () =>
            {
                try
                {
                    await _marketingContext.OrdersDetails.AddRangeAsync(orderDetailsList);
                    await _marketingContext.SaveChangesAsync();
                    return true;

                }catch(Exception e)
                {
                    return false;
                }


            });
        }

        public IEnumerable<OrderDetail> GetOrderDetail(string orderId)
        {
            IEnumerable<OrderDetail> orderDetail =
                              
                              from detail in _marketingContext.OrdersDetails
                              where detail.OrderId == orderId
                              select detail;

            return orderDetail;

        }








    }
}
