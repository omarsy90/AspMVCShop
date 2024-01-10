using Marketing.Areas.Identity.Data;
using Marketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Mvc;

namespace Marketing.Controllers
{

    [Authorize(Roles ="Default")]
    public class CustomerOrderController : Controller
    {


        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly UserManager<MarketingUser> _userManager;


        public CustomerOrderController(ICustomerOrderRepository customerOrderRepository, UserManager<MarketingUser> userManager)
        {
            _customerOrderRepository = customerOrderRepository;
            _userManager = userManager;
        }

        public async   Task< IActionResult> Index()
        {

         string userId =    _userManager.GetUserId(HttpContext.User);

            var orders = await _customerOrderRepository.GetOrderByCustomerId(userId);



            return View(orders);
        }

    }
}
