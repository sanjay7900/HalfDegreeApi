using BihariJe_WebApp.Models;
using BihariJe_WebApp.Services;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Net.Http.Headers;

namespace BihariJe_WebApp.ApiRepositories
{
    public class CustomerSite : ICustomer
    {
        HttpClient client;
        Uri baseaddress;
        public CustomerSite()
        {
            client = new HttpClient();
            baseaddress = new Uri("https://localhost:7242/api/");
            client.BaseAddress=baseaddress;
            client.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public CustomerSite(string jwt)
        {
            client = new HttpClient();
            baseaddress = new Uri("https://localhost:7242/api/");
            client.BaseAddress = baseaddress;
            client.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+jwt);
        }
        public bool AddOrders(Order order,string email)
        {
            order.IsDeliverd = false;
            order.IsApproved = false;
            order.Status = "Not Yet";
            order.ApprovedBy = "No One";
            order.DeliverdBy = "No One";
            order.Customer = email;
            order.DateTime= DateTime.Now;
            var data=JsonConvert.SerializeObject(order);
            var contentstring=new StringContent(data,System.Text.Encoding.UTF8,"application/json");
            HttpResponseMessage httpResponseMessage=client.PostAsync(baseaddress+"Orders/PostOrder",contentstring).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;   


        }

        public List<MyOrderResponse> GetMyorders(string customermail)
        {
            HttpResponseMessage httpResponseMessage = client.GetAsync( baseaddress+"Orders/MyOrders?customerEmail="+customermail).Result; 
            var myorders=httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<MyOrderResponse>>(myorders);
        }

        public bool RemoveOrders(int orderid)
        {
            throw new NotImplementedException();
        }

        public List<Product> SeeAllProducts()
        {
            return new AdminSite().GetProducts();


        }
        public Product GetProductById(int id)
        {
            return new AdminSite().GetProductById(id);
        }
    }
}
