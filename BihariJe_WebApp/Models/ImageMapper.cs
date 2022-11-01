using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BihariJe_WebApp.Models
{
    public class ImageMapper
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        [NotMapped]
        public IFormFile? ImageName { get; set; }
        public int? Quantity { get; set; }

    }
}
