using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HalfDegreeApi.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required, DataType(DataType.ImageUrl)]
        public string? ImageName { get; set; }
        [Required]
        public int? Quantity { get; set; }

    }
    
}
