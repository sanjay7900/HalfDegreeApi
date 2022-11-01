using HalfDegreeApi.Data;
using HalfDegreeApi.Models;
using HalfDegreeApi.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Linq;

namespace HalfDegreeApi.Repositories
{
    public class OrderData : IOrder<Order>
    {
       private  HalfDegreeApiDbContext _context;
        private readonly IProduct<Product> _productcontext;
        private readonly string url= "https://localhost:7242/";

        public OrderData(HalfDegreeApiDbContext halfDegreeApiDbContext,IProduct<Product> product)
        {
            _context = halfDegreeApiDbContext;
            _productcontext = product;
        }
        public bool AddOrder(Order order)
        {
            _context.orders!.Add(order);
            _context.SaveChanges();

            return true;

        }

        public List<MyOrderResponse> GetMyOrders(string customer)
        {
            var allorder = _context.orders!.Where(o => o.Customer == customer).ToList();
            var allproduct = _context.products!.ToList();
            var myorder = (from product in allproduct join order in allorder on product.Id equals order.ProductId orderby order.DateTime select new
            {
                order.Id,
                product.Name,
                product.ImageName,
                order.Price,
                order.Quantity,
                order.TotolPrice,
                order.DateTime,
                order.IsDeliverd,
                order.IsApproved,   
                order.Customer,
                order.MainAddress,
                
            } ).ToList()!;
            
            var data= JsonConvert.SerializeObject(myorder);
            var responseobject= JsonConvert.DeserializeObject<List<MyOrderResponse>>(data)!;
            foreach(var p in responseobject)
            {
                p.ImageName = url + p.ImageName;
            }
            return responseobject;  
        }

        public Order GetOrder(int orderId)
        {
            return _context.orders!.Where(o => o.Id == orderId).FirstOrDefault()!;
        }

        public List<Order> GetOrders()
        {
            return _context.orders!.Where(o => o.IsDeliverd==false && o.IsApproved==false).ToList();   
        }

        public bool IsApproved(int orderId,string adminId)
        {
            var order = GetOrder(orderId);
            order.IsApproved=true;
            order.ApprovedBy=adminId;
            _context.orders!.Update(order);
            _context.SaveChanges();

            return true;


        }

        public bool IsDelivered(int orderId,string deliverdpersonid)
        {
            var order = GetOrder(orderId);
            order.IsDeliverd = true;
            order.DeliverdBy=deliverdpersonid;
            _context.orders!.Update(order);
            _context.SaveChanges();

            return true;

        }
        public List<Order> ApprovedOrders()
        {
            var orderList = _context.orders!.Where(o => o.IsApproved == true && o.IsDeliverd == false).ToList();
            return orderList;
        }
        public List<Order> DeliverdOrder()
        {
            return _context.orders!.Where(o=>o.IsDeliverd==true).ToList();   
        }
        public List<Order> MyDeliverdOrder(string deliverByEmail)
        {
           return _context.orders!.Where(o=>o.IsDeliverd == true && o.DeliverdBy==deliverByEmail).ToList();
        }

    }
}
