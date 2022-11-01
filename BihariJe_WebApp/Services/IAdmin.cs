using BihariJe_WebApp.Models;

namespace BihariJe_WebApp.Services
{
    public interface IAdmin
    {
        public bool AddProduct(ImageMapper product);
        public bool UpdateProduct(ImageMapper product);
        public bool DeleteProduct(int productId); 
        public List<Product> GetProducts(); 
        public Product GetProductById(int productId);

    }
}
