using Marketing.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Runtime.CompilerServices;

namespace Marketing.Models
{
    public class OrderRepository : IOrderRepository , ICustomerOrderRepository
    {

        private readonly MarketingContext _marketingContext ;
     

        public OrderRepository(MarketingContext marketingContext)
        {

            _marketingContext = marketingContext;
        }  
        


         public  Task<bool> AddOrder(Order order)
        {

            return Task.Run(async () =>
            {
                try
                {
                    await _marketingContext.Orders.AddAsync(order);
                   await _marketingContext.SaveChangesAsync();
                    return true;

                }
                catch (Exception ex)
                {

                    return false;
                }

            });
            

        }

        public  Task< List<Order> >  GetOrders()
        {
            var marketingContext = _marketingContext.Orders.Include(o => o.Customer);
            return Task.Run(async () =>
            {
               var list = await marketingContext.ToListAsync();
                return list;
            });
                
                
              
        }





        // this is for customer repository 
        public async Task < IEnumerable<ViewModelOrder> > GetOrderByCustomerId(string customerId)
        {

            List<ViewModelOrder> viewModelOrderList = new List<ViewModelOrder>(); 
           
            var orders =  _marketingContext.Orders.Where(order=> order.CustomerId == customerId).Include(order=>order.OrderDetails).ToList();
           
            
              foreach(var order in orders)
            {
                ViewModelOrder viewModelOrder = new ViewModelOrder();
                viewModelOrder.OrderID = order.OrderId;
               
               
              
                viewModelOrder.CustomerEmail = _marketingContext.Customers.FirstOrDefault(cus => cus.ID == customerId)?.Email;

               viewModelOrder.Timestamp = order.Timestamp;

                viewModelOrder.StatusDeleviring = order.statusDeleviring;

                decimal sum = 0;

                List<ViewModelOrderDetail> details = new List<ViewModelOrderDetail>();


                foreach(var det in order.OrderDetails)
                {

                    ViewModelOrderDetail detail = new ViewModelOrderDetail();
                    detail.ProductImg = _marketingContext.Products.FirstOrDefault(pro => pro.ID == det.ProductId)?.ImgUrl;
                    detail.UnitPrice = (_marketingContext.Products.FirstOrDefault(pro => pro.ID == det.ProductId)?.ProductPrise);
                    detail.Quntity = det.Qauntity;

                    detail.Totalprice = detail.UnitPrice * detail.Quntity;

                    details.Add(detail);

                    // adding total price of specific product
                    sum += detail.Totalprice?? 0;


                }

                
                viewModelOrder.DetailsInfo = details;
                viewModelOrder.Total = sum;

                 viewModelOrderList.Add(viewModelOrder);



            }


            return viewModelOrderList;
        }

        public async  Task< Order> GetOrderById(string id)
        {


            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

       

                var order = await _marketingContext.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);

                return order; 

        

            

        }




        public async  Task< bool> EditStatus(string orderid , StatusDeleviring status)
        {
           
                Order order = await _marketingContext.Orders.FirstOrDefaultAsync(ord => ord.OrderId == orderid);
                if (order != null)
                {
                    try
                    {
                    order.statusDeleviring = status;
                    _marketingContext.SaveChanges();
                        return true;
                    }catch(Exception e)
                    {
                        return false;
                    }
                }
               

                return false;
         

            

        }



        public async Task<bool> DelelteOrder(string orderid)
        {
            
                Order order = await _marketingContext.Orders.Include(o=> o.OrderDetails).FirstOrDefaultAsync(ord => ord.OrderId == orderid);
                 
                if(order != null)
                {

                    try
                    {
                        _marketingContext.Orders.Remove(order);
                        _marketingContext.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }



                }

                //order is null
                return false;
               


               


          

        }

     
    }
}
