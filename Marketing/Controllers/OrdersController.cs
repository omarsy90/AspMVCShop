using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Marketing.DB;
using Marketing.Models;
using Microsoft.AspNetCore.Authorization;

namespace Marketing.Controllers
{

    [Authorize(Roles ="admin")]
    public class OrdersController : Controller
    {
        

        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetOrders();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
           var order  = await _orderRepository.GetOrderById(id);

            return View(order);
        }



        //// GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID");
        //    return View();
        //}

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderId,Total,CustomerId,statusDeleviring,Timestamp")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
        //    return View(order);
        //}





        // GET: Orders/Edit/5

        [HttpGet("Orders/Edit/{id?}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }


            var statuslist = new List<StatusDeleviring>() { StatusDeleviring.created, StatusDeleviring.processing, StatusDeleviring.delivering, StatusDeleviring.arrived };

          

            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }




            ViewBag.Status = statuslist;



            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Orderid, StatusDeleviring orderstatus)
        {
            Order order = await _orderRepository.GetOrderById(Orderid);
              
            if(order == null)
            {
                return NotFound();
            }


            bool isUpdated = await _orderRepository.EditStatus(Orderid, orderstatus);
                  if (isUpdated)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // exception while editing Order status

                ViewData["CustomerId"] = order.Customer.Email;

                return View(order);
            }
           
                
            
            
           
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var order = await  _orderRepository.GetOrderById(id);

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
               await _orderRepository.DelelteOrder(id);
            }
          
            return RedirectToAction(nameof(Index));
        }

    }
}
