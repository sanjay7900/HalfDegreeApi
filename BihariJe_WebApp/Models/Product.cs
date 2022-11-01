using System.ComponentModel.DataAnnotations;

namespace BihariJe_WebApp.Models
{
    public class Product
    {
        
        public int Id { get; set; }
       
        public string? Name { get; set; }
      
        public double Price { get; set; }
       
        public string? ImageName { get; set; }
        
        public int? Quantity { get; set; }
    }
}
