
using Marketing.Areas.Identity.Data;
using Marketing.DB;
using Marketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Configuration;
using Stripe.Checkout;

using System.Net;

namespace Marketing.Controllers
{


    [Authorize(Roles = "Default")]
    public class ShopingCardController : Controller
    {

        private readonly IShopingCardRepository _shopingCardRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly MarketingContext _marketingContext;
     
        private readonly UserManager<MarketingUser> _userManager;

        public ShopingCardController(IShopingCardRepository shopingCardRepository, IOrderRepository orderRepository,IOrderDetailRepository orderDetailRepository ,MarketingContext marketingContext,UserManager<MarketingUser> userManager)
        {
            _shopingCardRepository= shopingCardRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _marketingContext = marketingContext;
            _userManager= userManager; 
         
        }




        private IEnumerable<ViewModelSchpingCardItem> GetSchpingCardItemsForUser(string userId,out decimal total) 
        {

            var shpingCardItems = _shopingCardRepository.GetProductsInShopinfCard(userId);
             total = 0;

            List<ViewModelSchpingCardItem> itemsList = new List<ViewModelSchpingCardItem>();

            foreach (var item in shpingCardItems)
            {

                ViewModelSchpingCardItem viewModelSchpingCardItem = new ViewModelSchpingCardItem();

                viewModelSchpingCardItem.ProductId = item.ProductID;
                viewModelSchpingCardItem.ProductUrl = item.Product.ImgUrl;

                viewModelSchpingCardItem.UnitPrice = item.Product.ProductPrise;

                viewModelSchpingCardItem.Quantity = item.Quantity;
                viewModelSchpingCardItem.TotalPrice = viewModelSchpingCardItem.UnitPrice * viewModelSchpingCardItem.Quantity;

                itemsList.Add(viewModelSchpingCardItem);

                total += viewModelSchpingCardItem.TotalPrice;

            }

            return itemsList;

        }

        
        public ActionResult Index()
        {

            ViewBag.Total = 0;

          
            string useId = _userManager.GetUserId(HttpContext.User);

            if(useId == null)
            {
                return BadRequest("user is not verified");
            }


            decimal total = 0;

          var itemsList =  GetSchpingCardItemsForUser(useId, out total);
            ViewBag.Total = total;

            return View("Index",itemsList);


        }



        public JsonResult GetShopingCardItems()
        {

            string userId = _userManager.GetUserId(HttpContext.User);
            if(userId == null) 
            {
                Response.StatusCode = 401;
                return Json(false);
                    
                       
             }

            // authorized 


            decimal total = 0;

            var itemsList = GetSchpingCardItemsForUser(userId, out total);
            return Json(new {itemsList, Total=total});



        }




        [HttpPost]
        [Route("ShopingCard/AddProduct/{productId?}")]
        public JsonResult AddProduct( int productId)
        {

       




            string userId = _userManager.GetUserId(HttpContext.User);
            if(userId == null)
            {
                Response.StatusCode = 401;
                return Json(false);
            }

            //Auorized 
           

            string error = string.Empty;

            bool isSaved =   _shopingCardRepository.AddProduct(productId, userId);
            if(isSaved)
            {
                Response.StatusCode = 201;

             
            }
            else
            {
                // error in data sent with request
                Response.StatusCode = 400;
                error = "produckt not found ";
                
            }
            
           
            JsonResult json = new JsonResult(new {status = Response.StatusCode,error = error});
            return json;


        }


        [HttpDelete()]
        [Route("ShopingCard/RemoveItem/{productId?}")]
        public JsonResult RemoveItem(int productId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if(userId == null)
            {
                Response.StatusCode = 401;
            }
            else
            {
                bool isremoved = _shopingCardRepository.RemoveProduct(userId,productId);

                if (isremoved)
                {
                    Response.StatusCode = 200;
                }
                else
                {
                    Response.StatusCode = 400;
                }

               

            }

            return Json(new { status = Response.StatusCode });

        }



        /// <summary>
        /// checkout functinality 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>


        [HttpPost]
       
        public  RedirectResult   CreatCheckoutSession()
        {

            var userId = _userManager.GetUserId(HttpContext.User);
              
            if(userId == null)
            {
                //unAutorized 

                return Redirect("https://localhost:7127/home/error/401 you are not Authorized ,please singn in ");
            } 

            //Authorized 

            decimal amount = _shopingCardRepository.CalculateTheTotal(userId);

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                   new  SessionLineItemOptions
                   {
                       PriceData = new SessionLineItemPriceDataOptions
                       {
                           UnitAmount = Convert.ToInt32(amount)*100 ,
                           Currency="usd",
                           ProductData = new SessionLineItemPriceDataProductDataOptions
                           {
                               Name="Amount"
                           },
                       },



                       Quantity=1
                   }
                },

                //https://localhost:7127/ShopingCard/CreatCheckoutSession

                Mode = "payment",
                SuccessUrl = "https://localhost:7127/ShopingCard/success",
                CancelUrl = "https://localhost:7127/ShopingCard/cancel"

            };



            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session;
            try
            {
                session = service.Create(options);
                Response.Headers.Add("Location", session.Url);

              
                HttpContext.Session.SetString("purchased", "true");

                return  Redirect(session.Url);

            }
            catch(Exception e)
            {

               

              

            return    Redirect("https://localhost:7127/Home/error/404 Error check the amount ");
                
            }


       





        }



       public async Task<IActionResult>  Success()
        {
            

            string purchased = HttpContext.Session.GetString("purchased");
            if(purchased != "true")
            { 
                
            }
            HttpContext.Session.Remove("purchased");

            using (IDbContextTransaction transaction = _marketingContext.Database.BeginTransaction() )
            {


                string userId = _userManager.GetUserId(HttpContext.User);
                var shopingCardItems = _shopingCardRepository.GetProductsInShopinfCard(userId);
                decimal total = _shopingCardRepository.CalculateTheTotal(userId);

                Order order = new Order()
                {
                    OrderId = Guid.NewGuid().ToString(),
                    CustomerId = userId,
                    Total = total,
                    Timestamp = DateTime.UtcNow,

                };

                bool isOrderAdded = await _orderRepository.AddOrder(order);
                bool isDetailsAdded = false;
                bool isShpingCardItemsRemoved = false;

                List<OrderDetail> OrderDetilsList = new List<OrderDetail>();

                if (isOrderAdded == true)
                {

                    //Adding Order Details
                    foreach (var shop in shopingCardItems)
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = shop.ProductID,
                            Qauntity = shop.Quantity,
                        };

                        OrderDetilsList.Add(orderDetail);

                    }


                    //adding orderDetailsList to database 
                  isDetailsAdded=  await _orderDetailRepository.AddRange(OrderDetilsList);

                    if (isDetailsAdded)
                    {

                        // remove items in Shoping Cards 
                      isShpingCardItemsRemoved  =_shopingCardRepository.RemoveAllItems(userId);

                        if (isShpingCardItemsRemoved)
                        {
                            transaction.Commit();
                        }

                    }
              




                }

                if( ! (isOrderAdded && isDetailsAdded && isShpingCardItemsRemoved))
                {
                    transaction.Rollback();
                }



                return View("Success");




            }

            
        }

        private static void Canceled()
        {
         


        }



    }
}
