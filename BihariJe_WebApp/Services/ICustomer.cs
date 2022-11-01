using BihariJe_WebApp.Models;

namespace BihariJe_WebApp.Services
{
    public interface ICustomer
    {
        public List<Product> SeeAllProducts();
        public bool AddOrders(Order order,string email);
        public bool RemoveOrders(int orderid);
        public List<MyOrderResponse> GetMyorders(string customermail);


    }
}
