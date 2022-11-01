using BihariJe_WebApp.Models;
using BihariJe_WebApp.Services;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Drawing;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace BihariJe_WebApp.ApiRepositories
{
    public class AdminSite : IAdmin
    {
        private readonly Uri baseAddress;
        private readonly HttpClient httpClient;
       
        
        public AdminSite()
        {
            baseAddress = new Uri("https://localhost:7242/api/");
            httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
            httpClient.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));



        }
        public AdminSite(string jwt)
        {
            baseAddress = new Uri("https://localhost:7242/api/");
            httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
            httpClient.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt);

        }
        public  bool AddProduct(ImageMapper product)
        {
            var requestContent = new MultipartFormDataContent();
            var TakeImageAsStream = new MemoryStream();
            product.ImageName!.CopyTo(TakeImageAsStream);
            var img = Image.FromStream(TakeImageAsStream);
            
            ImageConverter imgCon = new ImageConverter();
            var imgdata= (byte[])imgCon.ConvertTo(img, typeof(byte[]));
            requestContent.Add(new ByteArrayContent(imgdata,0,imgdata.Length), "ImageName",product.ImageName.FileName);
           
            requestContent.Add(new StringContent(product.Name),"Name");
            requestContent.Add(new StringContent(product.Price.ToString()), "Price");
            requestContent.Add(new StringContent(product.Quantity.ToString()), "Quantity");

            HttpResponseMessage httpResponse = httpClient.PostAsync(baseAddress + "Products/PostProduct", requestContent).Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                TakeImageAsStream.Dispose();
                httpClient.Dispose();
                return true;
            }
            TakeImageAsStream.Dispose();
            httpClient.Dispose();
            return false;

            
        }

        public bool DeleteProduct(int productId)
        {
            HttpResponseMessage httpResponseMessage = httpClient.DeleteAsync(baseAddress + $"Products/DeleteProduct?id={productId}").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                httpClient.Dispose();

                return true;
            }
            httpClient.Dispose();

            return false;
        }

        public Product GetProductById(int productId)
        {
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + $"Products/GetProduct?id={productId}").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var getdata = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var productdata = JsonConvert.DeserializeObject<Product>(getdata);
                httpClient.Dispose();

                return productdata;
            }
            httpClient.Dispose();

            return new Product();
            
            
        }

        public List<Product> GetProducts()
        {
           HttpResponseMessage httpResponseMessage=httpClient.GetAsync(baseAddress+"Products/GetProducts").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string getData=httpResponseMessage.Content.ReadAsStringAsync().Result;  
                var listdata= JsonConvert.DeserializeObject<List<Product>>(getData);
                httpClient.Dispose();
                return listdata;

            }
            return new List<Product> { };
        }

        public bool UpdateProduct(ImageMapper product)
        {
            var jsondata=JsonConvert.SerializeObject(product);
            var updatedata = new StringContent(jsondata, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(baseAddress + "Products/PutProduct", updatedata).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                httpClient.Dispose();
                return true;
            }
            httpClient.Dispose();
            return false;
        }
        
    }
}
