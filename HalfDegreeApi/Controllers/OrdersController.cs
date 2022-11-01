using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HalfDegreeApi.Data;
using HalfDegreeApi.Models;
using HalfDegreeApi.Services;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.AspNetCore.Authorization;

namespace HalfDegreeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrder<Order> _context;

        public OrdersController(IOrder<Order> context)
        {
            _context = context;
        }

        // GET: api/Orders
        
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public string Getorders()
        {
            return JsonConvert.SerializeObject(_context.GetOrders());  
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public string GetOrder(int id)
        {
            var order =  _context.GetOrder(id);

            if (order == null)
            {
                return "error";
            }

            return JsonConvert.SerializeObject(order);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public string Approverorders()
        {
            return JsonConvert.SerializeObject(_context.ApprovedOrders());
        }
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult MyOrders(string customerEmail)
        {
            return Ok(JsonConvert.SerializeObject(_context.GetMyOrders(customerEmail)));    
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public bool ApproveOrder(int orderid, string admin)
        {
            return _context.IsApproved(orderid, admin);
          
           
        }
        [HttpPut]
        [Authorize(Roles = "DeliveryPerson")]
        public bool Deliver(int orderid, string deliverypersonid)
        {
            return _context.IsDelivered(orderid, deliverypersonid);


        }


        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult PostOrder(Order order)
        {
            _context.AddOrder(order);
            

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrder(int id)
        //{
        //    var order = await _context.orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.orders.Remove(order);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

       
    }
}
